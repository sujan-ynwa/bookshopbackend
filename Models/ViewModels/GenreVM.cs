using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models.ViewModels
{
    public class GenreVM
    {
        public Genre genre { get; set; }

        public IList<Book> books { get; set; }
    }
}
