using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Repositories
{
    internal interface IOrderRepository
    {
        Orders PlaceOrder(int userId, string deliveryAddress);

        Orders GetOrderById(int orderId);

        List<Orders> GetOrdersByUserId(int userId);

    }
}
