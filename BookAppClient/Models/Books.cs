using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAppClient.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book Title is required.")]
        [StringLength(100, ErrorMessage = "Book Title cannot be longer than 100 characters.")]
        public string BookTitle { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot be longer than 100 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters.")]
        public string Category { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Book Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int BookQuantity { get; set; }

        [RegularExpression(@"^(http|https):\/\/[^\s]*$", ErrorMessage = "Invalid URL format.")]
        [StringLength(250, ErrorMessage = "Book Cover URL cannot be longer than 250 characters.")]
        public string BookCover { get; set; }

        [Required(ErrorMessage = "Published Year is required.")]
        [Range(1900, 2100, ErrorMessage = "Published Year must be between 1900 and 2100.")]
        public int PublishedYear { get; set; }
    }
}