using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BookService.Models;
using BookService.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace BookService
{
    public class Startup
    {
        const string ServiceBusConnectionString = "Endpoint=sb://libraryservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=N+/IWwFIYrcbsO1/GJfKFUsefoiIwgRDtZqaHT+0zpo=";
        const string QueueName = "order-queue";
        static IQueueClient queueClient;

        static string _connString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


            //guide from https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            RegisterOnMessageHandlerAndReceiveMessages();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            _connString = Configuration.GetConnectionString("BookServiceContext");

            services.AddDbContext<BookServiceContext>(options =>
                    options.UseSqlServer(_connString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            //Allows to see which machine/docker container this instance is running on
            AddMachineNameToResponseHeader(app);

            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddMachineNameToResponseHeader(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("path", context.Request.Path.Value);
                    context.Response.Headers.Add("machine-name", Environment.MachineName);
                    return Task.FromResult(0);
                });
                await next();
            });
        }

        private void OnShutdown()
        {
            queueClient.CloseAsync();
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 5,

                AutoComplete = false
            };

            // Register the function that processes messages.
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            //Console.WriteLine($"Body:{Encoding.UTF8.GetString(message.Body)}");

            var jsonString = Encoding.UTF8.GetString(message.Body);

            var order = JsonConvert.DeserializeObject<Order>(jsonString);


            var optionsBuilder = new DbContextOptionsBuilder<BookServiceContext>();
            optionsBuilder.UseSqlServer(_connString);

            await using (var context = new BookServiceContext(optionsBuilder.Options))
            {
                var completedOrder = await context.CompletedOrder.FirstOrDefaultAsync(co => co.OrderId == order.Id, token);

                //Meaning the order havnt been fulfilled yet
                if (completedOrder == null)
                {
                    var orderedBook = await context.Book.FirstOrDefaultAsync(b => b.Id == order.BookId, token);

                    var physicalBook = new PhysicalBook
                    {
                        Book = orderedBook,
                        BookId = order.BookId
                    };

                    context.PhysicalBook.Add(physicalBook);

                    var completedOrder1 = new CompletedOrder
                    {
                        OrderId = order.Id
                    };
                    context.CompletedOrder.Add(completedOrder1);
                    await context.SaveChangesAsync(token);
                }
            }

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        // Use this handler to examine the exceptions received on the message pump.
        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
