

using WebMvc.Models.OrderModels;
using WebMVC.ViewModels;
using WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace WebMvc.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        //Task<List<Order>> GetOrdersByUser(ApplicationUser user);
        Task<Order> GetOrder(string orderId);
        Task<int> CreateOrder(Order order);
      //  Order MapUserInfoIntoOrder(ApplicationUser user, Order order);
      //  void OverrideUserInfoIntoOrder(Order original, Order destination);
    }
}
