using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models
{
    public class Author
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Please Don't leave this field empty")]
        public String  name { get; set; }
        public String  image_path { get; set; }

    }

}
