using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Views
{
    internal interface ILoginView
    {
        public TextBox LoginBox { get; set; }
        public TextBox PasswordBox { get; set; }
        public string LogIn { get; set; }
        public string Password { get; set; }

        event EventHandler VerifyCredentials;
        event EventHandler CloseWindow;
        event EventHandler CreateAccount;

        void Show();
        void Close();
    }
}
