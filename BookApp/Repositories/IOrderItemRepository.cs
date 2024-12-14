using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Repositories
{
    internal interface IOrderItemRepository
    {
        void AddOrderItem(OrderItem orderItem);
        void RemoveOrderItem(int orderitemid);

        List<OrderItem> GetAllOrderItems(int orderid);

    }
}
