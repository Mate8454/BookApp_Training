using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class Books
    {
        [Required]
        [Key]
        public int BookId { get; set; }

        [Required]
        public string BookTitile { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int BookQuantity { get; set; }

        [Required]
        public string BookCover { get; set; }

        [Required]
        
        public int PublishedYear { get; set; }
    }
}