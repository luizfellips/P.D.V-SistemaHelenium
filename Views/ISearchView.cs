using StockController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface ISearchView
    {
        //properties
        PrincipalModel MyModel { get; set; }
        bool SelectedFlag { get; set; }
        //Events
        event EventHandler SelectEvent;
        // Methods
        void SetProductListBindingSource(BindingSource productList);
       
        void ShowDialog();
    }
}
