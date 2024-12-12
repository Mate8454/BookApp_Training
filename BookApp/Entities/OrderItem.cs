using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
       
        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        
        [ForeignKey("Books")]
        public int BookId { get; set; }

        
        public int Quantity { get; set; }   
        public double OrderItemPrice { get; set; }


        public Orders Orders { get; set; } // Navigation Property
        public Books Books { get; set; }
    }
}