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
        void createUser(string username, string password);
    }
}
