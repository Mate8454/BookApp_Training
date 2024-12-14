using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Repositories
{
    internal interface ICartRepository
    {
        void AddToCart(Cart cart);
        void DeleteCartItem(int userid , int bookid);

        List<Cart> GetCartItems(int userid);

        

    }
}
