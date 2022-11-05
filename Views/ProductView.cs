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
using StockController.Repositories;
namespace StockController.Views
{
    public partial class ProductView : Form, IProductView
    {
        private string message;
        private bool isSuccessful;
        private bool isEdit;
        public ProductView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            FillComboboxes();
            tabControl1.TabPages.Remove(tabStockDetail);
            btnClose.Click += delegate { this.Close(); };
        }

        private void FillComboboxes()
        {
            Category categories = new Category();
            List<Category> CategoriesList = new List<Category>();
            CategoriesList = categories.GetCategories();
            foreach (var CategoryItem in CategoriesList)
            {
                categorySearch.Items.Add(CategoryItem.CategoryName);
                comboCategory.Items.Add(CategoryItem.CategoryName);
            }
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            categorySearch.KeyDown += (s,e) =>
            {
                if(e.KeyCode == Keys.Enter)
                {
                    SearchCategoryEvent?.Invoke(this, EventArgs.Empty);
                }
                
            };
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabStockInfo);
                tabControl1.TabPages.Add(tabStockDetail);
                tabStockDetail.Text = "Add new product";
            };
            // Edit
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabStockInfo);
                tabControl1.TabPages.Add(tabStockDetail);
                tabStockDetail.Text = "Edit product";
            };

            //Save
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabStockDetail);
                    tabControl1.TabPages.Add(tabStockInfo);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabStockDetail);
                tabControl1.TabPages.Add(tabStockInfo);
            };

            //Delete
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected product?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //Properties
        public string ProductId 
        {
            get { return txtProductId.Text; }
            set { txtProductId.Text = value; }
        }
        public string ProductName
        {
            get { return txtProductName.Text; }
            set { txtProductName.Text = value; }
        }
        public string ProductCategory 
        { 
            get { return comboCategory.Text; }
            set { comboCategory.Text = value; }
        }
        public string ProductQuantity
        {
            get { return txtQuantity.Text; }
            set { txtQuantity.Text = value; }
        }
        public string ProductPrice 
        { 
            get { return txtPrice.Text; }
            set { txtPrice.Text = value; }
        }
        public string SearchValue 
        { 
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }
        public string SearchCategoryValue
        {
            get { return categorySearch.Text; }
            set { categorySearch.Text = value; }
        }
        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccesful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }


        public event EventHandler SearchEvent;
        public event EventHandler SearchCategoryEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetProductListBindingSource(BindingSource productList)
        {
            dataGridView1.DataSource = productList;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "C";
            dataGridView1.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("pt-BR");

        }

        //singleton pattern

        private static ProductView instance;
        public static ProductView GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ProductView();
                instance.Show();
            }
            else
            {
                instance.Close();
                instance = new ProductView();
                instance.Show();
            }
            return instance;
        }
    }
}
