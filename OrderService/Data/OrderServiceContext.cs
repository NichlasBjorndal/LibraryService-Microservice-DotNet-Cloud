using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Models
{
    public class OrderServiceContext : DbContext
    {
        public OrderServiceContext (DbContextOptions<OrderServiceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var loopCount100Mb = 15000;

            var orders = new List<Order>();

            for (int i = 1; i < loopCount100Mb; i++)
            {
                var order1 = new Order
                {
                    Id = i,
                    OrderTime = default,
                    IsCompleted = false,
                    BookId = 1,
                };

                orders.Add(order1);
            }


            modelBuilder.Entity<Order>().HasData(
                orders
            );
        }

        public DbSet<OrderService.Models.Order> Order { get; set; }
    }
}
