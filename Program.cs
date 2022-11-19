using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockController.Models;
using StockController.Presenter;
using StockController.Views;
using StockController.Repositories;
using System.Configuration;


namespace StockController
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            ILoginView view = new LoginView();
            ILoginRepository repository = new LoginRepository(sqlConnectionString);
            new LoginPresenter(view, repository);
            Application.Run((Form)view);
        }
    }
}