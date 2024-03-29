﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockController.Models;
using StockController.Views;
using StockController.Repositories;

namespace StockController.Presenter
{
    internal class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;
        private int id;

        public MainPresenter(IMainView mainView, string sqlConnectionString, int id)
        {
            this.id = id;
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowStockView += ShowStockView;
            this.mainView.ShowPOSView += ShowPOSView;
            this.mainView.ShowUserView += ShowUserView;
        }

        private void ShowUserView(object? sender, EventArgs e)
        {
            IUserView view = UserView.GetInstance();
            IUserRepository repository = new UserRepository(sqlConnectionString);
            new UserPresenter(view,repository,this.id,mainView);
    }

        private void ShowPOSView(object? sender, EventArgs e)
        {
            IPrincipalView view = PrincipalView.GetInstance();
            IPrincipalRepository repository = new PrincipalRepository(sqlConnectionString);
            new PrincipalPresenter(view, repository, this.id);
        }

        private void ShowStockView(object? sender, EventArgs e)
        {
            IProductView view = ProductView.GetInstance();
            IProductRepository repository = new ProductRepository(sqlConnectionString);
            new ProductPresenter(view, repository);
        }
    }
}
