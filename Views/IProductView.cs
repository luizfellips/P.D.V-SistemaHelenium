using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    public interface IProductView
    {
        //properties
        string ProductId { get; set; }
        string ProductName { get; set; }
        string ProductCategory { get; set; }
        string ProductQuantity { get; set; }
        string ProductPrice { get; set; }
        string SearchValue { get; set; }
        string SearchCategoryValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccesful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler SearchCategoryEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        // Methods

        void SetProductListBindingSource(BindingSource productList);
        void Show();
    }
}
