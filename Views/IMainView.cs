using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface IMainView
    {
        event EventHandler ShowStockView;
        event EventHandler ShowPOSView;
        event EventHandler ShowUserView;

        void Show();
        void Close();
    }
}
