using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockController.Models;
using StockController.Repositories;
using StockController.Views;

namespace StockController.Presenter
{
    public class ProductPresenter
    {
        private IProductView view;
        private IProductRepository repository;
        private BindingSource productsBindingSource;
        private IEnumerable<ProductModel> productList;

        public ProductPresenter(IProductView view, IProductRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.productsBindingSource = new BindingSource();
            productsBindingSource.DataSource = repository.GetAll();
            this.view.SearchEvent += SearchProduct;
            this.view.SearchCategoryEvent += SearchCategory;
            this.view.AddNewEvent += AddNewProduct;
            this.view.EditEvent += LoadSelectedProduct;
            this.view.DeleteEvent += DeleteSelectedProduct;
            this.view.SaveEvent += SaveProduct;
            this.view.CancelEvent += CancelAction;
            this.view.SetProductListBindingSource(productsBindingSource);
            LoadAllProductsList();
        }
        private void SearchCategory(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchCategoryValue);
            if (emptyValue == false)
            {
                productList = repository.GetByCategory(this.view.SearchCategoryValue);
                productsBindingSource.DataSource = productList;
            }
            else
            {
                LoadAllProductsList();
            }
        }
        private void LoadAllProductsList()
        {
            productList = repository.GetAll();
            productsBindingSource.DataSource = productList;

        }
        private void SearchProduct(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
            {
                productList = repository.GetByValue(this.view.SearchValue);
                productsBindingSource.DataSource = productList;
            }
            else
            {
                LoadAllProductsList();
            }
        }
        private void CleanViewFields()
        {
            view.ProductId = "";
            view.ProductName = "";
            view.ProductQuantity = "";
            view.ProductCategory = "";
            view.ProductPrice = "";
        }
        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveProduct(object? sender, EventArgs e)
        {
            var model = new ProductModel();
            model.Id = Convert.ToInt32(view.ProductId);
            model.Name = view.ProductName;
            model.Category = view.ProductCategory;
            model.Quantity = Convert.ToInt32(view.ProductQuantity);
            model.Price = Convert.ToDouble(view.ProductPrice);

            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit) // edit model
                {
                    repository.Edit(model);
                    view.Message = "Product edited succesfully";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Product added succesfully";
                }
                view.IsSuccesful = true;
                LoadAllProductsList();
                CleanViewFields();

            }
            catch (Exception ex)
            {
                view.IsSuccesful = false;
                view.Message = "An error occurred while deleting.";
            }
        }

        private void DeleteSelectedProduct(object? sender, EventArgs e)
        {
            try
            {
                var product = (ProductModel)productsBindingSource.Current;
                repository.Delete(product.Id);
                view.IsSuccesful = true;
                view.Message = "Product deleted successfully";
                LoadAllProductsList();

            }
            catch (Exception ex)
            {
                view.IsSuccesful = false;
                view.Message = ex.ToString();
            }
        }

        private void LoadSelectedProduct(object? sender, EventArgs e)
        {
            var product = (ProductModel)productsBindingSource.Current;
            view.ProductId = product.Id.ToString();
            view.ProductName = product.Name;
            view.ProductCategory = product.Category;
            view.ProductQuantity = product.Quantity.ToString();
            view.ProductPrice = product.Price.ToString();
            view.IsEdit = true;
        }

        private void AddNewProduct(object? sender, EventArgs e)
        {
            view.IsEdit = false;

        }


    }
}
