using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Entities
{
    public class Cart
    {

        [Key]
        public int CartId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }

        public int Quantity { get; set; }
        

        public User User { get; set; } 
        public Books Book { get; set; }
        

    }
}