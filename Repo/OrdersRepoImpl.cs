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
    public class OrdersRepoImpl : OrdersRepo
    {
        private readonly IConfiguration _config;

        public OrdersRepoImpl(IConfiguration config)
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

        public string AddOrders(Orders order)
        {
           

            using (IDbConnection conn = Connection)
            {
                String query = "Select add_requested(@user_name, @book_title,@price, @isdelivered,@quantity);";
                conn.Open();
                conn.Query<Orders>(query, order);
                return "Insert Sucessful";
            }
        }

        public string DeleteOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Orders> GetAllOrders()
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select * from requested_books order by isdelivered";
                conn.Open();
                var orderList =   conn.Query<Orders>(query);
                return orderList.ToList();
            }
        }

        public Orders GetOrderById(int id)
        {
            Orders order = new Orders();

            using (IDbConnection conn = Connection)
            {
                String query = "Select * from requested_books where id= @id order by id";
                conn.Open();
                 order = conn.QueryFirstOrDefault<Orders>(query, new { id= id});
                return order;
            }
        }

        public string UpdateOrder(Orders order)
        {
            using (IDbConnection conn = Connection)
            {
                String query = "Select update_requested(@id,@user_name, @book_title, @price, @isdelivered,@quantity)";
                conn.Open();
                conn.Query<Orders>(query, order);
                return "Updated Sucessfully";
            }
        }
    }
}
