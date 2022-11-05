using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal interface IPrincipalRepository
    {
        void Delete(string value);
        IEnumerable<PrincipalModel> GetAll();
        void ClearRequest();
        PrincipalModel ReturnModel(string value);
        IEnumerable<PrincipalModel> GetByValue(string value, string quantity); // searches
    }
}
