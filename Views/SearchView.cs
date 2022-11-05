using StockController.Models;
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
    public partial class SearchView : Form,ISearchView
    {
        public bool selectedFlag = false;
        private PrincipalModel model = new PrincipalModel();
        public SearchView()
        {
           
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSelect.Click += delegate 
            { 
                SelectEvent?.Invoke(this, EventArgs.Empty);
                selectedFlag = true;
                this.Close();
            };
            btnCancel.Click += delegate
            { 
                this.Close();
            };
        }


        public event EventHandler SelectEvent;

        public bool SelectedFlag
        {
            get { return selectedFlag; }
            set { selectedFlag = value; }
        }
        public PrincipalModel MyModel
        {
            get { return model; }
            set { model = value; }
        }

        
        public void SetProductListBindingSource(BindingSource productList)
        {
            dataGridView1.DataSource = productList;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("pt-BR");

        }

        void ISearchView.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
