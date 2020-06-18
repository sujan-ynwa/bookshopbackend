using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models
{
    public class Orders
    {
        public int id { get; set; }
        public String  user_name { get; set; }
        public String book_title { get; set; }
        public decimal price { get; set; }
        public bool isDelivered { get; set; }
        public int quantity { get; set; }
    }
}
