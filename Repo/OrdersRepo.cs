using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;

namespace The_Book_Shop.Repo
{
   public interface OrdersRepo
    {

        IList<Orders> GetAllOrders();

        Orders GetOrderById(int id);

        String UpdateOrder(Orders order);

        String AddOrders(Orders orders);

        String DeleteOrderById(int id);
    }
}
