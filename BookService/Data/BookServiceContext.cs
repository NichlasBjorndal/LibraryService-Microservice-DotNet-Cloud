using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookService.Models;

namespace BookService.Models
{
    public class BookServiceContext : DbContext
    {
        public BookServiceContext (DbContextOptions<BookServiceContext> options)
            : base(options)
        {
        }

        public DbSet<BookService.Models.Book> Book { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var loopCount100Mb = 15000;

            var authors = new List<Author>();

            for (int i = 1; i < loopCount100Mb; i++)
            {
                var author1 = new Author
                {
                    Id = i,
                    FirstName = "Edger Allan",
                    LastName = "Poe"
                };


                authors.Add(author1);
            }

            var books = new List<Book>();

            for (int i = 1; i < loopCount100Mb; i++)
            {
                var book1 = new Book
                {
                    Id = i,
                    ISBN = "1234",
                    Title = "Book A",
                    AuthorId = 1
                };

                books.Add(book1);
            }

            modelBuilder.Entity<Author>().HasData(
                authors
            );


            modelBuilder.Entity<Book>(b =>
            {
                b.HasData(
                    books
                );

            });

        }



        public DbSet<BookService.Models.Author> Author { get; set; }



        public DbSet<BookService.Models.PhysicalBook> PhysicalBook { get; set; }



        public DbSet<BookService.Models.CompletedOrder> CompletedOrder { get; set; }
    }
}
