using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Models.ViewModels;
using The_Book_Shop.Repo;

namespace The_Book_Shop.Controllers
{
    public class GenreController :Controller
    {

        private GenreRepo _genreRepo;

        public GenreController(GenreRepo genre)
        {
            _genreRepo = genre;
        }

        //displays the table 
        public IActionResult Table()
        {
            var genres = _genreRepo.GetAllGenres();

            return View("GenreTable", genres);
        }


        // shows the 
        [Route("addGenre")]
        public IActionResult AddForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre genre)
        {
            if (ModelState.IsValid )
            { 
                _genreRepo.AddGenre(genre);
                return RedirectToAction("Table", genre);
            }
            else
            {
                return View("AddForm");
            }

        }

        [Route("genreUpdate/{id}")]
        public IActionResult UpdateForm(int id)
        {
            Genre toBeUpdated = _genreRepo.GetGenreById(id);


            return View(toBeUpdated);
        }


        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            if (ModelState.IsValid )
            {
                _genreRepo.UpdateGenre(genre);
                return RedirectToAction("Table", genre);
            }
            else
            {
               

                return View("UpdateForm", genre);
            }


        }

        [Route("genredetails/{id}")]
        public IActionResult ViewDetail(int id)
        {
            Genre genres = _genreRepo.GetGenreById(id);
            IList<Book> bookList = _genreRepo.GetBooksByGenre(id);

            

            GenreVM genreDetail = new GenreVM()
            {
                genre = genres,
                books = bookList
            };

            return View("Details", genreDetail);
        }


        [HttpGet]
        [Route("genrePopUp/{id}")]
        public IActionResult DeletePopUp(int id)
        {
            Genre genre = _genreRepo.GetGenreById(id);

            return View(genre);
        }

        // deletes the user
        [HttpGet]
        [Route("deletegenre/{id}")]
        public IActionResult DeleteById(int id)
        {
            // first retrive the data
            Genre toDelete = _genreRepo.GetGenreById(id);

            //then delete the data from the database
            _genreRepo.DeleteGenreById(id);

            return RedirectToAction("Table");

        }

    }
}
