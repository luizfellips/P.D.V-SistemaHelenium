using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal interface IUserRepository
    {
        void SaveSellingData(int id, string method, double value);
        UserModel GetUserInformations(int id);
        void CloseDayUser(int id);
        void CreateUser(LoginModel model);
    }
}
