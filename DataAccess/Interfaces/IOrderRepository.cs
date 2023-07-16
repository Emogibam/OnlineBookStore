using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        public bool CreateOrder(Order order);
        public List<Order> GetOrdersByUserId(Guid userId);
        public bool DeleteBook(Guid bookId);
        public bool UpdateOrderStatus(Guid orderId, string status);
    }
}
