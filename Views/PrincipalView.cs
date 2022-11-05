using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockController.Views
{
    public partial class PrincipalView : Form, IPrincipalView
    {
        private string message;
        public PrincipalView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            btnClose.Click += delegate { this.Close(); };
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(250, 100);
            
        }
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.F2:
                    var result = MessageBox.Show($"INICIAR NOVA VENDA? (ESSA AÇÃO LIMPARÁ A VENDA ATUAL).", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        ClearEvent?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(Message);
                    }
                    return true;
                case Keys.F5:
                     ClearBoxesEvent?.Invoke(this, EventArgs.Empty);
                    return true;
                       

            }

            return base.ProcessCmdKey(ref message, keys);
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnAddProduct.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtId.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            txtQuant.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            btnCancelItem.Click += delegate
            {
                var result = MessageBox.Show($"Are you sure you want to delete {CancelId} ID CODE PRODUCT?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    CancelProductEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            txtName.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SelectByNameEvent?.Invoke(this, EventArgs.Empty);

                }
            };
        }

        

        public event EventHandler SearchEvent;
        public event EventHandler SelectByNameEvent;
        public event EventHandler ClearEvent;
        public event EventHandler ClearBoxesEvent;
        public event EventHandler CancelProductEvent;

        public string ProductId 
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }
        public string ProductName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }
        public string ProductQuantity
        {
            get { return txtQuant.Text; }
            set { txtQuant.Text = value; }
        }
        public string ProductPrice
        {
            get { return txtUnitPrice.Text; }
            set { txtUnitPrice.Text = value; }
        }
        public string ProductTotalPrice
        {
            get { return txtTotalPrice.Text; }
            set { txtTotalPrice.Text = value; }
        }
        public string CancelId
        {
            get { return txtCancelId.Text; }
            set { txtCancelId.Text = value; }
        }
        public string TotalItems
        {
            get { return txtTotalItems.Text; }
            set { txtTotalItems.Text = value; }
        }
        public string TotalPrice
        {
            get { return txtTotal.Text; }
            set { txtTotal.Text = value; }
        }
        public string SearchValue
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private static PrincipalView instance;

        public static PrincipalView GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PrincipalView();
                instance.Show();
            }
            else
            {
                instance.Close();
                instance = new PrincipalView();
                instance.Show();
            }
            return instance;
        }

        public void SetProductListBindingSource(BindingSource productList)
        {
            dataGridView1.DataSource = productList;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("pt-BR");
            dataGridView1.Columns[4].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("pt-BR");
        }
    }
       
}
