using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface IPaymentView
    {
        //properties
        string PaymentMethod { get; set; }
        string PaymentPrice { get; set; }
        string PaidPrice { get; set; }
        string ChangePrice { get; set; }
        bool Finalized { get; set; }
        TextBox PaidBox { get; set; }

        //events
        event EventHandler FinalizeEvent;
        event EventHandler SettedMethodEvent;
        event EventHandler CancelEvent;
        event EventHandler ChangeEvent;

        //methods
        void ShowDialog();
    }
}
