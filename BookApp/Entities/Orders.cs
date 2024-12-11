using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class Orders
    {
        [Required]
        public int OrderId {  get; set; }
        [Required]
        public int UserId {  get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public double TotalAmount {  get; set; }
        [Required]
        public string Status {  get; set; }
        [Required]
        public string DeliveryAddress {  get; set; }
        

    }
}