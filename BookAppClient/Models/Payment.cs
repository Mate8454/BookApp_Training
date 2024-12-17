using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAppClient.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }


        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }

        //navigation

        public Orders Orders { get; set; }
    }
}