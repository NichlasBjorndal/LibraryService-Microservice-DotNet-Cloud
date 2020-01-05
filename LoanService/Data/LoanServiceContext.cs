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
            var loopCount100Mb = 15000;


            var loans = new List<Loan>();

            for (int i = 1; i < loopCount100Mb; i++)
            {

                var loan1 = new Loan
                {
                    Id = i,
                    UserId = 1,
                    BookId = 1,
                    StartDate = new DateTime(),
                    EndDate = new DateTime(),
                    Active = true
                };

                loans.Add(loan1);
            }


            modelBuilder.Entity<Loan>().HasData(
                loans
            );

        }
    }
}
