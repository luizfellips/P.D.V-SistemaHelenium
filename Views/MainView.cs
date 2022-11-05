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
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnEstoque.Click += delegate { ShowStockView?.Invoke(this, EventArgs.Empty); };
            btnPrincipal.Click += delegate { ShowPOSView?.Invoke(this, EventArgs.Empty); };
            btnClose.Click += delegate { this.Close(); };
            btnMinimize.Click += delegate { this.WindowState = FormWindowState.Minimized; };
        }

        public event EventHandler ShowStockView;
        public event EventHandler ShowPOSView;
        public event EventHandler ShowUserView;

        
    }
}
