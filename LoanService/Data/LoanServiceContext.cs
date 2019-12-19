using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Models
{
    public class LoanServiceContext : DbContext
    {
        public LoanServiceContext (DbContextOptions<LoanServiceContext> options)
            : base(options)
        {
        }

        public DbSet<LoanService.Models.Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var loan1 = new Loan
            {
                Id = 1,
                UserId = 1,
                BookId = 1,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
                Active = true
            };

            var loan2 = new Loan
            {
                Id = 2,
                UserId = 1,
                BookId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
                Active = false
            };


            var loan3 = new Loan
            {
                Id = 3,
                UserId = 2,
                BookId = 3,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
                Active = true
            };

            modelBuilder.Entity<Loan>().HasData(
             loan1, loan2, loan3
            );
        }
    }
}
