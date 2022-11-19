using StockController.Repositories;
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
    public partial class PaymentView : Form, IPaymentView
    {
        private bool finalized = false;
        public PaymentView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            FillComboboxes();
        }
        private void FillComboboxes()
        {
            PaymentMethod methods = new PaymentMethod();
            List<PaymentMethod> MethodsList = new List<PaymentMethod>();
            MethodsList = methods.GetMethods();
            foreach (var MethodItem in MethodsList)
            {
                comboPayment.Items.Add(MethodItem.Method);
            }
        }
        private void AssociateAndRaiseViewEvents()
        {
            btnFinalize.Click += delegate 
            { 
                FinalizeEvent?.Invoke(this, EventArgs.Empty);
                this.Close();

            };
            btnCancel.Click += delegate { this.Close(); };
            txtPaidPrice.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ChangeEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            comboPayment.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SettedMethodEvent?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public string PaymentMethod 
        {
            get { return comboPayment.Text; }
            set { comboPayment.Text = value; } 
        }
        public string PaymentPrice 
        {
            get { return txtPaymentPrice.Text; }
            set { txtPaymentPrice.Text = value; } 
        }
        public string PaidPrice
        {
            get { return txtPaidPrice.Text; }
            set { txtPaidPrice.Text = value; }
        }
        public string ChangePrice 
        {
            get { return txtChangePrice.Text; }
            set { txtChangePrice.Text = value; }
        }

        public bool Finalized 
        {
            get { return finalized; }
            set{ finalized = value; }
        }

        public TextBox PaidBox 
        {
            get => txtPaidPrice;
            set => txtPaidPrice = value; 
        }

        public event EventHandler FinalizeEvent;
        public event EventHandler ChangeEvent;
        public event EventHandler CancelEvent;
        public event EventHandler SettedMethodEvent;

        void IPaymentView.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
