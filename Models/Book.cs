using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models
{
    public class Book
    {
        public int id { get; set; }

        [Required]
        public String  title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please Select an Author")]
        public int author_id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please Select a Genre")]
        public int genre_id { get; set; }

        [Required(ErrorMessage = "Please give value in number")]
        [Range(0, 50000.00)]
        public decimal price { get; set; }

         public bool isavailable { get; set; }

        public String image_path { get; set; }

        [Required]
        public String description { get; set; }
    }
}
