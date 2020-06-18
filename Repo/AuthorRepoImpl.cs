using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using The_Book_Shop.Models;

namespace The_Book_Shop.Repo
{
    public class AuthorRepoImpl : AuthorRepo
    {
        private readonly IConfiguration _config;

        public AuthorRepoImpl(IConfiguration config)
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



        public string AddAuthor(Author author)
        {
           using (IDbConnection conn = Connection)
            {
                string Query = "Select  add_author(@name,@image_path)";
                conn.Open();
                conn.Query<Author>(Query, author);
                return "Insert Sucess";
            }
        }

        public string DeleteAuthorById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string Query = "Select delete_author(@id)";
                conn.Open();
                conn.Query<Author>(Query, new { id= id});
                return "Insert Sucess";
            }
        }

        public IList<Author> GetAllAuthors()
        {
            using (IDbConnection conn = Connection)
            {
                string Query = "Select * from author";
                conn.Open();
                var result =   conn.Query<Author>(Query);
                return result.ToList();
            }
        }

        public Author GetAuthorById(int id)
        {
            Author author = new Author();
            using (IDbConnection conn = Connection)
            {
                string Query = "Select * from author where id = @id";
                conn.Open();
                author = conn.QueryFirstOrDefault<Author>(Query,new {id = id });
                return author;
            }
        }

        public IList<Book> GetBookByAuthor(int author_id)
        {
           using (IDbConnection conn = Connection)
            {
                String query = "Select * from book where author_id = @author_id order by id";
                conn.Open();
                var bookList = conn.Query<Book>(query, new { author_id = author_id });
                return bookList.ToList();
            }
        }

        public string UpdateAuthor(Author author)
        {
            using (IDbConnection conn = Connection)
            {
                string Query = "Select * From update_author(@id,@name,@image_path)";
                conn.Open();
                conn.Query<Author>(Query, author);
                return "Insert Sucess";
            }
        }
    }
}
