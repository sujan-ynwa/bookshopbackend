using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models.ViewModels
{
    public class BookTable
    {
        public int id { get; set; }
        public String title { get; set; }

        public String genre { get; set; }

        public String author { get; set; }

        public decimal price { get; set; }

        public bool isavailable { get; set; }

        public String image_path { get; set; }

        public String  description { get; set; }
    }
}
