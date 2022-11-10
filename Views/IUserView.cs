using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface IUserView
    {
        //properties
        string UserName { get; set; }
        string TotalSellings { get; set; }
        string SoldValue { get; set; }
        string CancelledSellings { get; set; }
        string MoneySellings { get; set; }
        string DebitSellings { get; set; }
        string CreditSellings { get; set; }
        string PixSellings { get; set; }

        //events
        event EventHandler VisualizeStockEvent;
        event EventHandler CloseDayEvent;
        event EventHandler ChangeUserEvent;

        //properties
        void Show();
    }
}
