using StockController.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Repositories
{
    internal class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void createUser(string username, string password)
        {
            string HashedPassword = GenerateHashMD5(password);
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into UsersCredentials values(@username,@password)";
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                command.Parameters.Add("@password", SqlDbType.VarChar).Value = HashedPassword;
                command.ExecuteNonQuery();
            }
        }

        public int getUser(string username, string password)
        {
            string HashedPassword = GenerateHashMD5(password);
            string stringName = "";
            int id = 0;
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "if @password = (select PasswordHash from UsersCredentials where Username = @user) select Username from UsersCredentials where Username = @user";
                command.Parameters.Add("@user", SqlDbType.VarChar).Value = username;
                command.Parameters.Add("@password", SqlDbType.VarChar).Value = HashedPassword;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stringName += reader[0].ToString();
                    }
                }
            }
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select id from Users where userName = @userName";
                command.Parameters.Add("@userName", SqlDbType.VarChar).Value = stringName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id += Convert.ToInt32(reader[0]);
                    }
                }
            }
            return id;

        }

        public static string GenerateHashMD5(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool checkUser(string username)
        {
            List<string> userList = new List<string>();
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select userName from UsersCredentials";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(reader[0].ToString());
                    }
                }
            }
            if (userList.Contains(username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
