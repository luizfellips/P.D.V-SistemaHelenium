using StockController.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Repositories
{
    internal class Category
    {
        public string CategoryName { get; set; }
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        public List<Category> GetCategories()
        {
            List<Category> categoriesList = new List<Category>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select CategoryName from Products_Categories";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryName = reader["CategoryName"].ToString();
                        categoriesList.Add(category);
                    }
                }
            }
            return categoriesList;
        }
    }
}
