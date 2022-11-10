using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace StockController.Repositories
{
    internal class PaymentMethod
    {
        public string Method { get; set; }
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        public List<PaymentMethod> GetMethods()
        {
            List<PaymentMethod> methodsList = new List<PaymentMethod>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select methodName from PaymentMethods";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PaymentMethod method = new PaymentMethod();
                        method.Method = reader["methodName"].ToString();
                        methodsList.Add(method);
                    }
                }
            }
            return methodsList;
        }
    }
}
