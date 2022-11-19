using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockController.Views
{
    public partial class LoginView : Form, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnLogin.Click += delegate { VerifyCredentials?.Invoke(this, EventArgs.Empty); };
            btnCreateAcc.Click += delegate { CreateAccount?.Invoke(this, EventArgs.Empty); };
            PasswordBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    VerifyCredentials?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public string LogIn 
        {
            get => txtUsername.Text;
            set => txtUsername.Text = value;
        }
        public string Password 
        { 
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }
        public TextBox LoginBox 
        {
            get => txtUsername; 
            set => txtUsername = value;
        }
        public TextBox PasswordBox 
        {
            get => txtPassword; 
            set => txtPassword = value;
        }

        public event EventHandler VerifyCredentials;
        public event EventHandler CloseWindow;
        public event EventHandler CreateAccount;
    }
}
