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
    internal class LoginPresenter
    {
        private ILoginRepository repository;
        private ILoginView view;

        public LoginPresenter(ILoginView view, ILoginRepository repository)
        {
            this.repository = repository;
            this.view = view;
            this.view.VerifyCredentials += CheckAccount;
            this.view.CreateAccount += CreateAccountView;
        }

        private void CheckAccount(object? sender, EventArgs e)
        {
            int id = repository.getUser(this.view.LogIn,this.view.Password);
            if(id == 0)
            {
                MessageBox.Show("Credenciais incorretas");
            }
            else
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                IMainView mainview = new MainView();
                new MainPresenter(mainview, sqlConnectionString,id);
                mainview.Show();
            }
            this.view.LoginBox.Clear();
            this.view.PasswordBox.Clear();
            
        }

        private void CreateAccountView(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
