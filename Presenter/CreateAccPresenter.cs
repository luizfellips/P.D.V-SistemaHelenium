using StockController.Models;
using StockController.Views;
using StockController.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace StockController.Presenter
{
    internal class CreateAccPresenter
    {
        private ICreateAccView view;
        private ILoginRepository repository;
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        public CreateAccPresenter(ICreateAccView view, ILoginRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.view.CreateEvent += CreateAccount;
            this.view.ShowDialog();
        }

        private void CreateAccount(object? sender, EventArgs e)
        {
            bool flag = repository.checkUser(this.view.Username);
            if(!flag)
            {
                repository.createUser(this.view.Username, this.view.Password);
                IUserRepository userRepo = new UserRepository(connectionString);
                LoginModel model = new LoginModel();
                model.Username = this.view.Username;
                model.Password = this.view.Password;
                userRepo.CreateUser(model);
                this.view.Message = "Account created successfully";
            }
            else
            {
                this.view.Message = "This username is already taken, try again!";
            }
        }
    }
}
