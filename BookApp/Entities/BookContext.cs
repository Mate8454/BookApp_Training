using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookApp.Entities
{
    public class BookContext : DbContext

    {
        public BookContext():base("OnlineBookAppConnection") { }

        public DbSet<Books> Books { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Payment> Payment { get; set; }

    }
}