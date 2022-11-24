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
    public partial class CreateAccView : Form, ICreateAccView
    {
        private string message;
        public string Message 
        { 
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        public CreateAccView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnCreate.Click += delegate 
            { 
                CreateEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate { this.Close(); };
        }

        public string Username 
        {
            get => txtUsername.Text;
            set => txtUsername.Text = value;
        }
        public string Password 
        {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }

        public event EventHandler CreateEvent;

        void ICreateAccView.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
