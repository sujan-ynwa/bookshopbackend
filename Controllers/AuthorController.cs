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
    public class AuthorController : Controller
    {

        private AuthorRepo _authourRepo;
        private IWebHostEnvironment _hostEnvironment;

        public AuthorController(IWebHostEnvironment hostEnvironment, AuthorRepo authorRepo)
        {
            _authourRepo = authorRepo;
            _hostEnvironment = hostEnvironment;
        }


        //displays the table 
        public IActionResult Table()
        {
            var authors = _authourRepo.GetAllAuthors();
            return View("AuthorTable", authors);
        }


        // shows the 
        [Route("addAuthor")]
        public IActionResult AddForm(AuthorVM authors)
        {
            return View(authors);
        }

        [HttpPost]
        public IActionResult Add(AuthorVM authors)
        {
            if (ModelState.IsValid && authors.image != null)
            {
                // this class contains the function that handles the files
                FileHandling fileHandling = new FileHandling(_hostEnvironment);
                String imagePath = fileHandling.fileUpload(authors.image, "authors");

                authors.author.image_path = imagePath;
                _authourRepo.AddAuthor(authors.author);
                return RedirectToAction("Table", authors);
            }
            else
            {
                authors.fileError = "Please Upload a Image";
                return View("AddForm", authors);
            }

        }

        [Route("authordetails/{id}")]
        public IActionResult ViewDetail(int id)
        {
            Author authors = _authourRepo.GetAuthorById(id);
            IList<Book> bookList = _authourRepo.GetBookByAuthor(id);

            Console.WriteLine("------------------"+bookList);

            AuthorVM authorDetail = new AuthorVM()
            {
                author = authors,
                books = bookList
            };

            return View("Details", authorDetail);
        }


        [Route("authorUpdate/{id}")]
        public IActionResult UpdateForm(int id)
        {
            Author toBeUpdated = _authourRepo.GetAuthorById(id);



            AuthorVM authorVM = new AuthorVM()
            {
                author = toBeUpdated,
            };
            return View(authorVM);
        }


        [HttpPost]
        public IActionResult Update(AuthorVM authors)
        {
            if (ModelState.IsValid)
            {
                // if user doesnot uplaod new image
                String imagePath = _authourRepo.GetAuthorById(authors.author.id).image_path;

               
                // if the user Uploads new Image 
                if (authors.image != null)
                {
                    // this class contains the function that handles the files
                    FileHandling fileHandling = new FileHandling(_hostEnvironment);
                    imagePath = fileHandling.fileUpload(authors.image, "authors");
                }
                

                authors.author.image_path = imagePath;
                _authourRepo.UpdateAuthor(authors.author);
                return RedirectToAction("Table", authors);
            }
            else
            {
                return View("UpdateForm", authors);
            }


        }


        [HttpGet]
        [Route("authorPopUp/{id}")]
        public IActionResult DeletePopUp(int id)
        {
            Author author = _authourRepo.GetAuthorById(id);

            return View(author);
        }

        // deletes the user
        [HttpGet]
        [Route("deleteauthor/{id}")]
        public IActionResult DeleteById(int id)
        {
            // first retrive the data
            Author authorToDelete = _authourRepo.GetAuthorById(id);

            //then delete the data from the database
            _authourRepo.DeleteAuthorById(id);

            // deleting the user file 
            FileHandling fileHandling = new FileHandling(_hostEnvironment);
            fileHandling.deleteFile(authorToDelete.image_path, "authors");

            return RedirectToAction("Table");

        }

    }
}
