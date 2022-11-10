using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface IStockView
    {
        void SetProductListBindingSource(BindingSource stockList);
        void ShowWindow();
    }
}
