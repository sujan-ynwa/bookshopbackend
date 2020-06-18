using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Book_Shop.Models.ViewModels
{
    public class AuthorVM
    {
        public Author author { get; set; }

        public IList<Book> books { get; set; }

        public IFormFile image { get; set; }

        public String fileError { get; set; }

    }
}
