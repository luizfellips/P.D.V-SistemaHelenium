using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface ICreateAccView
    {
        string Message { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        event EventHandler CreateEvent;
        void ShowDialog();
    }
}
