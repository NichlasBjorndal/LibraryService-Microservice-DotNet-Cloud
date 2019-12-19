using System;

namespace BookService.Models.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrdTime { get; set; }
        public bool IsCompleted { get; set; }
        public int BookId { get; set; }
    }
}
