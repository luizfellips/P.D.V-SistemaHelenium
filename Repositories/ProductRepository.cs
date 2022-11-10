using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using StockController.Models;

namespace StockController.Repositories
{
    internal class ProductRepository : BaseRepository, IProductRepository
    {
        public string CategoryName { get; set; }
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;

        }

        public void Add(ProductModel productmodel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Products values (@id, @name, @category, @quantity, @price)";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productmodel.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productmodel.Name;
                command.Parameters.Add("@category", SqlDbType.NVarChar).Value = productmodel.Category;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = productmodel.Quantity;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = productmodel.Price;
                command.ExecuteNonQuery();

            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from Products where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();

            }
        }

        public void Edit(ProductModel productmodel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Products 
set Product_Name = @name, Product_Category = @category, Product_Quantity = @quantity, Product_Price = @price
where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productmodel.Id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productmodel.Name;
                command.Parameters.Add("@category", SqlDbType.NVarChar).Value = productmodel.Category;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = productmodel.Quantity;
                command.Parameters.Add("@price", SqlDbType.Decimal).Value = productmodel.Price;
                command.ExecuteNonQuery();

            }
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update StockControl 
                                        set EndQuantity = @quantity
                                        where id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productmodel.Id;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = productmodel.Quantity;
                command.ExecuteNonQuery();

            }
        }

            public IEnumerable<ProductModel> GetAll()
        {
            var productList = new List<ProductModel>();
            using(var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Products order by Product_Category asc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.Id = (int)reader[0];
                        productModel.Name = reader[1].ToString();
                        productModel.Category = reader[2].ToString();
                        productModel.Quantity = (int)reader[3];
                        productModel.Price = Convert.ToDouble(reader[4]);
                        productList.Add(productModel);
                    }
                }
            }

            return productList;

        }

        public IEnumerable<ProductModel> GetByCategory(string category)
        {
            var productList = new List<ProductModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Products Where Product_Category = @category";
                command.Parameters.Add("@category", SqlDbType.NVarChar).Value = category;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.Id = (int)reader[0];
                        productModel.Name = reader[1].ToString();
                        productModel.Category = reader[2].ToString();
                        productModel.Quantity = (int)reader[3];
                        productModel.Price = Convert.ToDouble(reader[4]);
                        productList.Add(productModel);
                    }
                }
            }

            return productList;
        }

        public IEnumerable<ProductModel> GetByValue(string value)
        {
            var productList = new List<ProductModel>();
            int productId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string productName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from Products 
                                        where Product_Id = @id or Product_Name like '%'+@name+'%'
                                        order by CHARINDEX(@name,Product_Name)";
                command.Parameters.Add("@id", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = productName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productModel = new ProductModel();
                        productModel.Id = (int)reader[0];
                        productModel.Name = reader[1].ToString();
                        productModel.Category = reader[2].ToString();
                        productModel.Quantity = (int)reader[3];
                        productModel.Price = Convert.ToDouble(reader[4]);
                        productList.Add(productModel);
                    }
                }
            }
            return productList;
        }

        public void UpdateQuantity(int id, int quantity)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Update Products set Product_Quantity = Product_Quantity - @quantity where Product_Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                command.ExecuteNonQuery();
            }
        }
    }
}
