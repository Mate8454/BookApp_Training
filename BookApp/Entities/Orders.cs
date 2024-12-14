using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId {  get; set; }
        [ForeignKey("User")]
        public int UserId {  get; set; }

       

        public DateTime OrderDate { get; set; }
        
        public int TotalPrice { get; set; }
        
        public string Status {  get; set; }
        
        public string DeliveryAddress {  get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();



    }
}