using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookApp.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
      
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        [Required]
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }

        //navigation
        
        public Orders Orders { get; set; }
    }
}