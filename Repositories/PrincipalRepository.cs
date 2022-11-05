using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using StockController.Models;
using System.Reflection;

namespace StockController.Repositories
{
    internal class PrincipalRepository : BaseRepository, IPrincipalRepository
    {
        public PrincipalRepository(string connectionString)
        {
            this.connectionString = connectionString;

        }

        public void ClearRequest()
        {
            using(var connection = new SqlConnection(connectionString))
                using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from Request";
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string value)
        {
            int id = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from Request where RequestId = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PrincipalModel> GetAll()
        {
            List<PrincipalModel> productList = new List<PrincipalModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Request";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new PrincipalModel();
                        model.Id = (int)reader[0];
                        model.Name = reader[1].ToString();
                        model.Quantity = (int)reader[2];
                        model.Price = Convert.ToDouble(reader[3]);
                        model.Total = Convert.ToDouble(reader[4]);
                        productList.Add(model);
                    }
                }
            }
            return productList;
        }

        public IEnumerable<PrincipalModel> GetByValue(string value, string quantity)
        {
            var model = new PrincipalModel();
            IEnumerable<PrincipalModel> productList;
            int productId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            int productQuantity = int.TryParse(quantity, out _) ? Convert.ToInt32(quantity) : 0;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from Products 
                                        where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.Id = (int)reader[0];
                        model.Name = reader[1].ToString();
                        model.Quantity = productQuantity;
                        model.Price = Convert.ToDouble(reader[4]);
                        model.Total = model.Quantity * model.Price;
                    }
                }
            }
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Request values (@id, @name, @quantity, @price)";
                command.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = model.Name;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = model.Quantity;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = model.Price;
                command.ExecuteNonQuery();
            }
            productList = GetAll();
            return productList;
        }
        public PrincipalModel ReturnModel(string value)
        {
            var model = new PrincipalModel();
            int productId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from Products 
                                        where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Id = (int)reader[0];
                        model.Name = reader[1].ToString();
                        model.Quantity = (int)reader[3];
                        model.Price = Convert.ToDouble(reader[4]);
                        model.Total = model.Quantity * model.Price;
                    }
                }
            }
            return model;
        }
    }
}
