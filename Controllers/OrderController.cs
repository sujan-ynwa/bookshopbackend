using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Repo;

namespace The_Book_Shop.Controllers
{
    public class OrderController : Controller
    {
        private OrdersRepo _orderRepo;

        public OrderController(OrdersRepo ordersRepo)
        {
            _orderRepo = ordersRepo;
        }


        //displays the table
        public IActionResult Table()
        {
            var ordersList = _orderRepo.GetAllOrders();

            Console.WriteLine(ordersList.Count);
            return View("OrdersTable", ordersList);
               
        }

        [Route("orderUpdate/{id}")]
        public IActionResult UpdateForm(int id)
        {
            Orders toBeUpdated = _orderRepo.GetOrderById(id);
            return View(toBeUpdated);
        }


        [HttpPost]
        public IActionResult Update(Orders order)
        {
            if (ModelState.IsValid)
            {
                _orderRepo.UpdateOrder(order);
                return RedirectToAction("Table", order);
            }
            else
            {
                return View("UpdateForm", order);
            }


        }

    }
}
