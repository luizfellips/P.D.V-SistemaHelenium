using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockController.Views;
using StockController.Models;
using StockController.Repositories;

namespace StockController.Presenter
{
    internal class StockPresenter
    {
        private IStockView view;
        private IStockRepository repository;
        private BindingSource stockBindingSource;

        public StockPresenter(IStockView view, IStockRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.stockBindingSource = new BindingSource();
            this.stockBindingSource.DataSource = this.repository.GetAll();
            this.view.SetProductListBindingSource(stockBindingSource);
            this.view.ShowWindow();
        }
    }
}
