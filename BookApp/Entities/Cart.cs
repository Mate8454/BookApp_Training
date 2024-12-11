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
        [Required]
        public int CartId {  get; set; }
        [Required]
        [ForeignKey(nameof(Books))]
        public int BookId { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}