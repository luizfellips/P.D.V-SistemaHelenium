using StockController.Models;
using StockController.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockController.Models;
using StockController.Repositories;
using StockController.Views;
using System.Configuration;

namespace StockController.Presenter
{
    internal class PrincipalPresenter
    {
        private IPrincipalView view;
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        private IPrincipalRepository repository;
        private int id;
        private BindingSource productsBindingSource;
        private IEnumerable<StockModel> stocklist = new List<StockModel>();
        private IEnumerable<PrincipalModel> productList = new List<PrincipalModel>();
      
        public PrincipalPresenter(IPrincipalView view, IPrincipalRepository repository, int id)
        {
            this.id = id;
            this.view = view;
            this.repository = repository;
            this.productsBindingSource = new BindingSource();
            productsBindingSource.DataSource = repository.GetAll();
            productList = repository.GetAll();
            this.view.SearchEvent += SearchProduct;
            this.view.CancelProductEvent += CancelItem;
            this.view.SelectByNameEvent += SelectByName;
            this.view.ClearEvent += ClearAll;
            this.view.PaymentEvent += PaymentWindow;
            this.view.ClearBoxesEvent += ClearBoxesEvent;
            this.view.SetProductListBindingSource(productsBindingSource);
            ClearBoxes();
            this.view.ProductQuantity = "1";
        }

        private void PaymentWindow(object? sender, EventArgs e)
        {
            IPaymentView paymentView = new PaymentView();
            IProductRepository productRepo = new ProductRepository(connectionString);
            double totalValue = productList.ToList().Sum(item => item.Total);
            new PaymentPresenter(paymentView, productRepo, totalValue,this.id);
            if(paymentView.Finalized)
            {
                MessageBox.Show("VENDA COMPLETADA COM SUCESSO.");
                repository.ClearRequest();
                productList = repository.GetAll();
                productsBindingSource.DataSource = repository.GetAll();
                ClearBoxes();
            }

        }

        private void ClearBoxesEvent(object? sender, EventArgs e)
        {
            ClearBoxes();
        }

        private void SelectByName(object? sender, EventArgs e)
        {
            PrincipalModel model = new PrincipalModel();
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.ProductQuantity);
            if (!emptyValue)
            {
                ISearchView searchView = new SearchView();
                IProductRepository productRepo = new ProductRepository(connectionString);
                new SearchPresenter(searchView, productRepo, this.view.SearchValue, this.view.ProductQuantity);
                if (searchView.SelectedFlag)
                {
                    model = searchView.MyModel;
                    productList = repository.GetByValue(model.Id.ToString(), model.Quantity.ToString());
                    productsBindingSource.DataSource = productList;
                    view.ProductId = model.Id.ToString();
                    view.ProductName = model.Name;
                    view.ProductPrice = $"R${model.Price}";
                    view.ProductTotalPrice = $"R${(Convert.ToInt32(view.ProductQuantity) * model.Price)}";
                    view.TotalItems = $"{productList.Count()}";
                    view.TotalPrice = $"R${productList.ToList().Sum(item => item.Total)}";
                    
                }
            }
            else
            {
                MessageBox.Show("Quantity is missing");
            }

            
        }

        private void ClearAll(object? sender, EventArgs e)
        {
            repository.ClearRequest();
            productList = repository.GetAll();
            productsBindingSource.DataSource = repository.GetAll();
            ClearBoxes();
            view.Message = "VENDA RESETADA COM SUCESSO";
        }

        private void SearchProduct(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.ProductId);
            if (!emptyValue)
            {
                productList = repository.GetByValue(this.view.ProductId, this.view.ProductQuantity);
                productsBindingSource.DataSource = productList;
                PrincipalModel model = repository.ReturnModel(this.view.ProductId);
                view.ProductName = model.Name;
                view.ProductPrice = $"R${model.Price.ToString()}";
                view.ProductTotalPrice = $"R${(Convert.ToInt32(view.ProductQuantity) * model.Price).ToString()}";
                view.TotalItems = $"{productList.Count().ToString()}";
                view.TotalPrice = $"R${productList.ToList().Sum(item => item.Total).ToString()}";
                
            }   

            else
            {
                MessageBox.Show("An error occurred. Perhaps you forgot the quantity? ");
            }
        }
        private void CancelItem(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.CancelId);
            if(!emptyValue)
            {
                try
                {
                    repository.Delete(this.view.CancelId);
                    productsBindingSource.DataSource = repository.GetAll();
                    productList = repository.GetAll();
                    ClearBoxes();
                    view.Message = $"item deleted successfully";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
        }
        
        private void ClearBoxes()
        {
            view.ProductId = "";
            view.ProductName = "";
            view.ProductPrice = "";
            view.ProductQuantity = "";
            view.ProductTotalPrice = "";
            view.CancelId = "";
            view.TotalItems = $"{productList.Count().ToString()}";
            view.TotalPrice = $"R${productList.ToList().Sum(item => item.Total).ToString()}";
        }
        


    }
    
}
