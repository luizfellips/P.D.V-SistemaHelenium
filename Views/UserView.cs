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
    public partial class UserView : Form, IUserView
    {
        public UserView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            btnClose.Click += delegate { this.Close(); };
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(250, 100);
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnVisualize.Click += delegate { VisualizeStockEvent?.Invoke(this, EventArgs.Empty); };
            btnFecharCaixa.Click += delegate
            {
                var result = MessageBox.Show($"DESEJA FECHAR O CAIXA? ISSO LIMPARÁ O ESTOQUE INTEIRO", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    CloseDayEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("ESTOQUE LIMPO COM SUCESSO");
                };
            };
        }

        public string UserName
        { 
            get => txtUser.Text;
            set => txtUser.Text = value;
        }
        public string TotalSellings 
        { 
            get => txtSellings.Text; 
            set => txtSellings.Text = value;
        }
        public string SoldValue 
        { 
            get => txtSold.Text;
            set => txtSold.Text = value; 
        }
        public string CancelledSellings
        {
            get => txtCancelled.Text; 
            set => txtCancelled.Text = value; 
        }
        public string MoneySellings
        {
            get => txtMoney.Text;
            set => txtMoney.Text = value;
        }
        public string DebitSellings 
        {
            get => txtDebit.Text; 
            set => txtDebit.Text = value; 
        }
        public string CreditSellings
        {
            get => txtCredit.Text;
            set => txtCredit.Text = value; 
        }
        public string PixSellings 
        { 
            get => txtPix.Text;
            set => txtPix.Text = value;
        }


        public event EventHandler VisualizeStockEvent;
        public event EventHandler CloseDayEvent;
        public event EventHandler ChangeUserEvent;

        private static UserView instance;

        public static UserView GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new UserView();
                instance.Show();
            }
            else
            {
                instance.Close();
                instance = new UserView();
                instance.Show();
            }
            return instance;
        }

        
    }
}
