using StockController.Models;
using StockController.Repositories;
using StockController.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Presenter
{
    internal class UserPresenter
    {
        private IUserView view;
        private IUserRepository repository;
        private int id;
        private IMainView mainView;
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        public UserPresenter(IUserView view, IUserRepository repository, int id, IMainView mainView)
        {
            this.mainView = mainView;
            this.id = id;
            this.view = view;
            this.repository = repository;
            this.view.VisualizeStockEvent += OpenStock;
            this.view.CloseDayEvent += CloseDay;
            this.view.ChangeUserEvent += ChangeUser;
            UserModel model = this.repository.GetUserInformations(this.id);
            this.view.UserName = model.Name;
            this.view.TotalSellings = model.TotalSellings.ToString();
            this.view.CancelledSellings = model.CancelledSellings.ToString();
            this.view.SoldValue = $"R${model.TotalSoldValue}";
            this.view.CreditSellings = $"R${model.CreditSellings}";
            this.view.DebitSellings = $"R${model.DebitSellings}";
            this.view.PixSellings = $"R${model.PixSellings}";
            this.view.MoneySellings = $"R${model.MoneySellings}";
        }

        private void ChangeUser(object? sender, EventArgs e)
        {
            this.view.Close();
            this.mainView.Close();
        }

        private void CloseDay(object? sender, EventArgs e)
        {
            IStockRepository stockRepository = new StockRepository(connectionString);
            stockRepository.ClearDayStockControl();
            repository.CloseDayUser(this.id);
            UserModel model = this.repository.GetUserInformations(this.id);
            this.view.UserName = model.Name;
            this.view.TotalSellings = model.TotalSellings.ToString();
            this.view.CancelledSellings = model.CancelledSellings.ToString();
            this.view.SoldValue = $"R${model.TotalSoldValue}";
            this.view.CreditSellings = $"R${model.CreditSellings}";
            this.view.DebitSellings = $"R${model.DebitSellings}";
            this.view.PixSellings = $"R${model.PixSellings}";
            this.view.MoneySellings = $"R${model.MoneySellings}";

        }

        private void OpenStock(object? sender, EventArgs e)
        {
            IStockView view = new StockControlView();
            IStockRepository stockRepository = new StockRepository(connectionString);
            new StockPresenter(view, stockRepository);
        }
    }
}
