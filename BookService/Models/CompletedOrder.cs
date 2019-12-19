using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookService.Models
{
    public class CompletedOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
    }
}
