using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Models.ViewModels;
using The_Book_Shop.Repo;

namespace The_Book_Shop.Controllers
{
    public class BookController : Controller
    {

        private BookRepo _bookrepo;

        // for  list of aurthor while adding books
        private AuthorRepo _authorRepo;
        // for list of genre while adding books
        private GenreRepo _genreRepo;

        private IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepo bookRepo, AuthorRepo authors, GenreRepo genres, IWebHostEnvironment webHostEnvironment)
        {
            _bookrepo = bookRepo;
            _authorRepo = authors;
            _genreRepo = genres;
            _webHostEnvironment = webHostEnvironment;
        }

        //displays the table 
        public IActionResult Table()
        {
            var Books = _bookrepo.GetBookTable();

            return View("BookTable", Books);
        }


        // shows the 
        [Route("addBook")]
        public IActionResult AddForm(BookVM book)
        {

            book.genreList = _genreRepo.GetAllGenres();
            book.authorList = _authorRepo.GetAllAuthors();

            return View("AddForm", book);

        }

        [HttpPost]
        public IActionResult Add(BookVM book)
        {
            if (ModelState.IsValid && book.image != null)
            {
                // this class contains the function that handles the files
                FileHandling fileHandling = new FileHandling(_webHostEnvironment);
                String imagePath = fileHandling.fileUpload(book.image, "books");

                book.books.image_path = imagePath;
                _bookrepo.AddBook(book.books);
                return RedirectToAction("Table", book);
            }
            else
            {
                book.fileError = "Please Upload a Image";
                book.genreList = _genreRepo.GetAllGenres();
                book.authorList = _authorRepo.GetAllAuthors();

                return View("AddForm", book);
            }

        }

        [Route("bookdetails/{id}")]
        public IActionResult ViewDetail(int id)
        {
            BookTable book = _bookrepo.GetBookTableById(id);
;
            return View("Details", book);
        }


        [Route("bookUpdate/{id}")]
        public IActionResult UpdateForm(int id)
        {
            Book toBeUpdated = _bookrepo.GetById(id);
          

            BookVM bookVM = new BookVM()
            {
                books = toBeUpdated,
                authorList = _authorRepo.GetAllAuthors(),
                genreList = _genreRepo.GetAllGenres()
             };
            return View(bookVM);
        }

       
        [HttpPost]
        public IActionResult Update(BookVM book)
        {
            if(ModelState.IsValid)
            {
                // if the image file is new updated
                String imagePath = _bookrepo.GetById(book.books.id).image_path;

                // if new image is updated
                if (book.image != null)
                {
                    // this class contains the function that handles the files
                    FileHandling fileHandling = new FileHandling(_webHostEnvironment);
                    imagePath = fileHandling.fileUpload(book.image, "books");
                }
                

                book.books.image_path = imagePath;
                _bookrepo.UpdateBook(book.books);
                return RedirectToAction("Table", book);
            }
            else
            {
                book.fileError = "Please Upload a Image";
                book.genreList = _genreRepo.GetAllGenres();
                book.authorList = _authorRepo.GetAllAuthors();

                return View("UpdateForm", book);
            }

           
        }


        [HttpGet]
        [Route("bookPopUp/{id}")]
        public IActionResult DeletePopUp(int id)
        {
            Book book = _bookrepo.GetById(id);

            return View(book);
        }

        // deletes the user
        [HttpGet]
        [Route("deleteBook/{id}")]
        public IActionResult DeleteById(int id)
        {
            // first retrive the data
            Book bookToDelete = _bookrepo.GetById(id);

            //then delete the data from the database
            _bookrepo.DeleteBookById(id);

            // deleting the user file 
            FileHandling fileHandling = new FileHandling(_webHostEnvironment);
            fileHandling.deleteFile(bookToDelete.image_path, "books");

            return RedirectToAction("Table");

        }



    }
}