using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApp.Entity
{
    public class Book
    {
        [Required]
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishYear { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string CoverImage { get; set; }

        [Required]
        public int Quantity { get; set; }



    }
}