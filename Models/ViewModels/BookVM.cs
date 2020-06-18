using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models.ViewModels
{
    public class BookVM
    {
        public Book books { get; set; }

        public IList<Author> authorList { get; set; }

        public IList<Genre> genreList { get; set; }

        public IFormFile image { get; set; }

        public String fileError { get; set; }

    }
}
