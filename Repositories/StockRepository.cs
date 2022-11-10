using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockController.Models;
using StockController.Repositories;

namespace StockController.Repositories
{
    internal class StockRepository : BaseRepository, IStockRepository
    {
        public StockRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ClearDayStockControl()
        {
            using(var connection = new SqlConnection(connectionString))
                using(var command = new SqlCommand())
                {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from StockControl";
                command.ExecuteNonQuery();
                }
        }

        public void FirstInitializationAdd(int id, string name, int quantity)
        {
            int initialQuantity = 0;
            int subtractQuantity = quantity;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select Product_Quantity from Products where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        initialQuantity += (int)reader[0];
                    }
                }
            }

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into StockControl values(@id,@name,@startQuantity,@endQuantity)";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                command.Parameters.Add("@startQuantity", SqlDbType.Int).Value = initialQuantity;
                command.Parameters.Add("@endQuantity", SqlDbType.Int).Value = initialQuantity - subtractQuantity;
                command.ExecuteNonQuery();
            }

        }

        public List<StockModel> GetAll()
        {
            List<StockModel> stockList = new List<StockModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from StockControl";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockModel model = new StockModel();
                        model.Id = (int)reader[0];
                        model.Name = reader[1].ToString();
                        model.StartQuantity = (int)reader[2];
                        model.EndQuantity = (int)reader[3];
                        stockList.Add(model);
                    }
                }
            }
            return stockList;
        }

        public void UpdateInfos(int id, int quantity)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Update StockControl
                                        set EndQuantity = EndQuantity - @quantity 
                                        where id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                command.ExecuteNonQuery();
            }
        }
    }
}
