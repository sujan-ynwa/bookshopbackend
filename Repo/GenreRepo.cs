using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;

namespace The_Book_Shop.Repo
{
    public interface GenreRepo
    {

        IList<Genre> GetAllGenres();

        Genre GetGenreById(int id);

        IList<Book> GetBooksByGenre(int id);

        String UpdateGenre(Genre genre);

        String AddGenre(Genre genre);

        String DeleteGenreById(int id);
    }
}
