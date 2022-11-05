using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    public interface IProductRepository
    {
        void Add(ProductModel petmodel);
        void Edit(ProductModel petmodel);
        void Delete(int id);
        IEnumerable<ProductModel> GetAll();
        IEnumerable<ProductModel> GetByValue(string value); // searches
        IEnumerable<ProductModel> GetByCategory(string category);
    }
}
