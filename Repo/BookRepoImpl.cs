using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;
using The_Book_Shop.Models.ViewModels;

namespace The_Book_Shop.Repo
{
    public class BookRepoImpl : BookRepo
    {
        private readonly IConfiguration _config;

        public BookRepoImpl(IConfiguration config)
        {
            _config = config;
        }

        //creating the connection with the database  
        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_config.GetConnectionString("DatabaseConnection"));
            }
        }

        public string AddBook(Book book)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT add_book(@title, @author_id, @genre_id, @price, @isavailable, @image_path,@description)";
                conn.Open();
                conn.Query<Book>(sQuery, book);
                return "Insert Success";
            }
        }

        public string DeleteBookById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT delete_book(@id)";
                conn.Open();
                conn.Query<Book>(sQuery, new { id = id });
                return "Delete Sucessful";
            }
        }

        public IList<Book> GetAllBooks()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * from book";
                conn.Open();
                var result = conn.Query<Book>(sQuery);
                return result.ToList();
            }
        }


         public BookTable GetBookTableById(int id)
        {
            BookTable book = new BookTable();
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * from bookTable where id = @id";
                conn.Open();
                book = conn.QueryFirstOrDefault<BookTable>(sQuery,new{ id= id});
                return book;
            }
        }

        public IList<BookTable> GetBookTable()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * from booktable order by id";
                conn.Open();
                var book = conn.Query<BookTable>(sQuery);
                return book.ToList();
            }
        }

        public Book GetById(int id)
        {
            Book book = new Book();

            using (IDbConnection conn = Connection)
            {
                String sQuery = "SELECT * from book where id = @id ";
              
                conn.Open();
                book = conn.QueryFirstOrDefault<Book>(sQuery, new { id = id });

               

                return book;
            }
        }

        public string UpdateBook(Book book)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT update_book(@id,@title, @author_id, @genre_id, @price, @isavailable, @image_path,@description)";
                conn.Open();
                conn.Query<Book>(sQuery, book);
                return "Insert Success";
            }
        }

    }
}
