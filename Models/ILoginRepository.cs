using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal interface ILoginRepository
    {
        int getUser(string username, string password);
        bool checkUser(string username);
        void createUser(string username, string password);
    }
}
