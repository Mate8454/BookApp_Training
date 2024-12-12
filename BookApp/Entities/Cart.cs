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
        
        public int CartId {  get; set; }
       
       
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public double CartTotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public User User { get; set; } 
        

    }
}