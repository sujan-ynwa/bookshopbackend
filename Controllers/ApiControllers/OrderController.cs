using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Repo;

namespace The_Book_Shop.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        private OrdersRepo _orderRepo;

        public OrderController(OrdersRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }


        [HttpPost]
        [Route("add")]
        public String Add([FromBody] Orders orders)
        {
            

            try
            {   
                _orderRepo.AddOrders(orders);
                return "Order Sucessful";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
                
            }
        }



    }
}
