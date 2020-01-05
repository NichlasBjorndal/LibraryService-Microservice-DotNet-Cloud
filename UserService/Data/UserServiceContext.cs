using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserServiceContext : DbContext
    {
        public UserServiceContext (DbContextOptions<UserServiceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var loopCount100Mb = 15000;

            var users = new List<User>();

            for (int i = 1; i < loopCount100Mb; i++)
            {

                var user1 = new User
                {
                    Id = i,
                    firstName = "Nick",
                    lastName = "Hansen",
                    email = "abc@gmail.com",
                    password = "thisIsAPW"
                };

                users.Add(user1);
            }


            modelBuilder.Entity<User>().HasData(
                users
            );
        }

        public DbSet<UserService.Models.User> User { get; set; }
    }
}
