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
            var author1 = new Author
            {
                Id = 1,
                FirstName = "Edger Allan",
                LastName = "Poe"
            };

            var book1 = new Book
            {
                Id = 1,
                ISBN = "1234",
                Title = "Book A",
                AuthorId = 1
            };

            var author2 = new Author
            {
                Id = 2,
                FirstName = "Thomas",
                LastName = "Edison"
            };
            var book2 = new Book
            {
                Id = 2,
                ISBN = "1234",
                Title = "Book B",
                AuthorId = 2
            };

            var author3 = new Author
            {
                Id = 3,
                FirstName = "H.C",
                LastName = "Andersen"
            };

            var book3 = new Book
            {
                Id = 3,
                ISBN = "1234",
                Title = "Book Z",
                AuthorId = 3
            };

            modelBuilder.Entity<Author>().HasData(
                author1, author2, author3
            );

            modelBuilder.Entity<Book>(b =>
            {
                b.HasData(
                    book1, book2, book3
                );

            });

        }



        public DbSet<BookService.Models.Author> Author { get; set; }



        public DbSet<BookService.Models.PhysicalBook> PhysicalBook { get; set; }



        public DbSet<BookService.Models.CompletedOrder> CompletedOrder { get; set; }
    }
}
