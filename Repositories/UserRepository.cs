using StockController.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockController.Repositories
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CloseDayUser(int id)
        {
            using(var connection = new SqlConnection(connectionString))
                using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Update users
                                        set totalsellings = 0, 
                                            cancelledsellings = 0, 
                                            pixselling = 0,
                                            debitselling = 0, 
                                            creditselling = 0,
                                            moneyselling = 0
                                        where id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public UserModel GetUserInformations(int id)
        {
            UserModel model = new UserModel();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Users where id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Id += (int)reader[0];
                        model.Name += reader[1].ToString();
                        model.TotalSellings += (int)reader[2];
                        model.CancelledSellings += (int)reader[3];
                        model.PixSellings += Convert.ToDouble(reader[4]);
                        model.DebitSellings += Convert.ToDouble(reader[5]);
                        model.CreditSellings += Convert.ToDouble(reader[6]);
                        model.MoneySellings += Convert.ToDouble(reader[7]);
                        model.TotalSoldValue += Convert.ToDouble(reader[8]);
                    }
                }
            }
            return model;
        }

        public void SaveSellingData(int id, string method, double totalValue)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                switch(method)
                {
                    case "Pix":
                        command.CommandText = "Update Users set pixSelling = @totalValue, totalSellings = totalSellings + 1 where id = @id";
                        command.Parameters.Add("@totalValue",SqlDbType.Decimal).Value = totalValue;
                        command.Parameters.Add("@id", SqlDbType.Decimal).Value = id;
                        break;
                    case "Credit":
                        command.CommandText = "Update Users set creditSelling = @totalValue, totalSellings = totalSellings + 1 where id = @id";
                        command.Parameters.Add("@totalValue", SqlDbType.Decimal).Value = totalValue;
                        command.Parameters.Add("@id", SqlDbType.Decimal).Value = id;
                        break;
                    case "Debit":
                        command.CommandText = "Update Users set debitSelling = @totalValue, totalSellings = totalSellings + 1 where id = @id";
                        command.Parameters.Add("@totalValue", SqlDbType.Decimal).Value = totalValue;
                        command.Parameters.Add("@id", SqlDbType.Decimal).Value = id;
                        break;
                    case "Money":
                        command.CommandText = "Update Users set moneySelling = @totalValue, totalSellings = totalSellings + 1 where id = @id";
                        command.Parameters.Add("@totalValue", SqlDbType.Decimal).Value = totalValue;
                        command.Parameters.Add("@id", SqlDbType.Decimal).Value = id;
                        break;
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
