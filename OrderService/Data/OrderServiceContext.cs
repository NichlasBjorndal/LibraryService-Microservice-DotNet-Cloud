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
            var order = new Order
            {
                Id = 2,
                OrderTime = default,
                IsCompleted = false,
                BookId = 7,
               
            };

            var order2 = new Order
            {
                Id = 3,
                OrderTime = default,
                IsCompleted = false,
                BookId = 8,
            };


            modelBuilder.Entity<Order>().HasData(
                order, order2
            );
        }

        public DbSet<OrderService.Models.Order> Order { get; set; }
    }
}
