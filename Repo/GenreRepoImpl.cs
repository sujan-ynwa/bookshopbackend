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
    public class GenreRepoImpl : GenreRepo
    {
        private readonly IConfiguration _config;

        public GenreRepoImpl(IConfiguration config)
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

        public string AddGenre(Genre genre)
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select add_genre(@name)";
                conn.Open();
                conn.Query<Genre>(query, genre);
                return "Insert Sucessful";
            }
        }

        public string DeleteGenreById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select delete_genre(@id)";
                conn.Open();
                conn.Query<Genre>(query, new { id= id});
                return "Insert Sucessful";
            }
        }

        public IList<Genre> GetAllGenres()
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select * from genre order by id";
                conn.Open();
                var result =  conn.Query<Genre>(query);
                return result.ToList();
            }
        }

        public IList<Book> GetBooksByGenre(int genre_id)
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select * from book where genre_id=@genre_id order by id";
                conn.Open();
                var result = conn.Query<Book>(query,new {genre_id = genre_id });
                return result.ToList();
            }
        }

        public Genre GetGenreById(int id)
        {
            Genre genre = new Genre();
            using (IDbConnection conn = Connection)
            {
                String query = "Select * from genre where id = @id";
                conn.Open();
                genre = conn.QueryFirstOrDefault<Genre>(query , new { id= id});
                return genre;
            }
        }

        public string UpdateGenre(Genre genre)
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select update_genre(@id,@name)";
                conn.Open();
                conn.Query<Genre>(query, genre);
                return "update Sucessful";
            }
        }
    }
}
