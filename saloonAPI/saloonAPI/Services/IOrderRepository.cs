using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders();
        public Order SaveOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(Order order);
        public Order GetOrder(int orderId);
        public void UpdateStatus(Order order, int status);
    }
}
