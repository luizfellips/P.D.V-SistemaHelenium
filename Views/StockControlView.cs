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
    public partial class StockControlView : Form, IStockView
    {
        public StockControlView()
        {
            InitializeComponent();
        }

        public void SetProductListBindingSource(BindingSource stockList)
        {
            dataGridView1.DataSource = stockList;
        }


        void IStockView.ShowWindow()
        {
            this.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
