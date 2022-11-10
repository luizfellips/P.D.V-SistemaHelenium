using StockController.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockController.Repositories;
using StockController.Models;
using System.Configuration;
using System.Reflection;

namespace StockController.Presenter
{
    internal class PaymentPresenter
    {
        private IPaymentView view;
        private IProductRepository repository;
        private double totalValue;
        private List<StockModel> stockList = new List<StockModel>();
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        internal List<StockModel> StockList 
        { 
            get => stockList;
            set => stockList = value;
        }

        public PaymentPresenter(IPaymentView view, IProductRepository repository, double totalValue)
        {
            this.view = view;
            this.repository = repository;
            this.totalValue = totalValue;
            this.view.FinalizeEvent += FinalizePayment;
            this.view.ChangeEvent += SetChange;
            this.view.PaymentPrice = $"R${Math.Truncate(totalValue*100)/100}";
            this.view.ShowDialog();
        }

        private void SetChange(object? sender, EventArgs e)
        {
            double totalPrice = this.totalValue;
            double paidPrice = Convert.ToDouble(this.view.PaidPrice);
            double changePrice = paidPrice - totalPrice;
            this.view.ChangePrice = $"R${Math.Truncate(changePrice * 100) / 100}";
            this.view.PaidPrice = $"R${Math.Truncate(paidPrice * 100) / 100}";
        }

        private void FinalizePayment(object? sender, EventArgs e)
        {
            try
            {
                IStockRepository stockControl = new StockRepository(connectionString);
                IUserRepository userRepository = new UserRepository(connectionString);
                StockList = stockControl.GetAll();
                IPrincipalRepository principalRepository = new PrincipalRepository(connectionString);
                StockModel stockmodel = new StockModel();
                foreach (PrincipalModel model in principalRepository.GetAll())
                {
                    bool containsItem = StockList.Any(x => x.Id == model.Id && x.Name == model.Name);
                    if (containsItem == false)
                    {
                        stockControl.FirstInitializationAdd(model.Id, model.Name, model.Quantity);
                    }
                    else
                    {
                        stockControl.UpdateInfos(model.Id, model.Quantity);
                    }
                    repository.UpdateQuantity(model.Id, model.Quantity);
                }
                userRepository.SaveSellingData(1, this.view.PaymentMethod, totalValue);
                this.view.Finalized = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.view.Finalized = false;
            }
        }
    }
}
