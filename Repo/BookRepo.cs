using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Models.ViewModels;

namespace The_Book_Shop.Repo
{
   public interface BookRepo
    {
        IList<BookTable> GetBookTable();

        IList<Book> GetAllBooks();

        // for the table view
        BookTable GetBookTableById(int id);

        // for book
        Book GetById(int id);

        String UpdateBook(Book book);

        String AddBook(Book book);

        String DeleteBookById(int id);


    }
}
