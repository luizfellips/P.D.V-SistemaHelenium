using StockController.Models;
using StockController.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Presenter
{
    internal class SearchPresenter
    {
        private ISearchView view;
        private IProductRepository repository;
        private BindingSource productsBindingSource;
        private string quantity;
        

        public SearchPresenter(ISearchView view, IProductRepository repository,string value, string quantity)
        {
            this.view = view;
            this.repository = repository;
            this.quantity = quantity;
            this.productsBindingSource = new BindingSource();
            productsBindingSource.DataSource = this.repository.GetByValue(value);
            this.view.SelectEvent += GetProduct;
            this.view.SetProductListBindingSource(productsBindingSource);
            this.view.ShowDialog();
        }

        

        public void GetProduct(object? sender, EventArgs e)
        {
            var prodModel = (ProductModel)productsBindingSource.Current;
            PrincipalModel prinModel = new PrincipalModel();
            prinModel.Id = prodModel.Id;
            prinModel.Name = prodModel.Name;
            prinModel.Price = prodModel.Price;
            prinModel.Quantity = Convert.ToInt32(quantity);
            prinModel.Total = prinModel.Quantity * prinModel.Price;
            this.view.MyModel = prinModel;
        }
    }
}
