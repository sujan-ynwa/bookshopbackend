using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;

namespace The_Book_Shop.Repo
{
  public interface AuthorRepo
    {
        IList<Author> GetAllAuthors();

        IList<Book> GetBookByAuthor(int authorId);

        Author GetAuthorById(int id);

        String UpdateAuthor(Author author);

        String AddAuthor(Author author);

        String DeleteAuthorById(int id);
    }
}
