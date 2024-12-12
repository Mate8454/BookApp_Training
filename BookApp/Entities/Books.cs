using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class Books
    {
        
        [Key]
        public int BookId { get; set; }

       
        public string BookTitile { get; set; }
      
        public string Author { get; set; }

        
        public double Price { get; set; }

       
        public string Category { get; set; }

        
        public string Description { get; set; }

       
        public int BookQuantity { get; set; }

        
        public string BookCover { get; set; }

        
        
        public int PublishedYear { get; set; }
    }
}