using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface IPrincipalView
    {
        string ProductId { get; set; }
        string ProductName { get; set; }
        string ProductQuantity { get; set; }
        string ProductPrice { get; set; }
        string ProductTotalPrice { get; set; }
        string CancelId { get; set; }
        string TotalItems { get; set; }
        string TotalPrice { get; set; }
        string SearchValue { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler PaymentEvent;
        event EventHandler SearchEvent;
        event EventHandler SelectByNameEvent;
        event EventHandler CancelProductEvent;
        event EventHandler ClearBoxesEvent;
        event EventHandler ClearEvent;
        // Methods

        void SetProductListBindingSource(BindingSource productList);
        void Show();
    }
}
