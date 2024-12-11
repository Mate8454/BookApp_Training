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
        public string BookTitle { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishedYear { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string CoverImage { get; set; }

        [Required]
        public string Category { get; set; }
    }
}