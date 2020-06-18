using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The_Book_Shop.Models.ViewModels;
using The_Book_Shop.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace The_Book_Shop.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private BookRepo _bookRepo;

        public BookController(BookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }
        // GET: api/<controller>

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<BookTable> GetAll()
        {
            return _bookRepo.GetBookTable();
        }

        // GET api/<controller>/5
        [HttpGet("[action]/{id}")]
        public BookTable Get(int id)
        {

            return _bookRepo.GetBookTableById(id);
        }


    }

       
}
