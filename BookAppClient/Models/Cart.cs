using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookAppClient.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
        [ForeignKey("Book")]
        public string BookCover { get; set; }

        public User User { get; set; }
        public Books Book { get; set; }
    }
}