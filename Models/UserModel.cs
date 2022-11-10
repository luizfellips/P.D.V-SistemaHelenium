using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal class UserModel
    {
        private int id;
        private string name;
        private int totalSellings;
        private int cancelledSellings;
        private double pixSellings;
        private double creditSellings;
        private double debitSellings;
        private double moneySellings;
        private double totalSoldValue;
        

        public int Id 
        { 
            get => id;
            set => id = value; 
        }
        public string Name 
        { 
            get => name;
            set => name = value;
        }
        public int TotalSellings 
        { 
            get => totalSellings; 
            set => totalSellings = value;
        }
        public int CancelledSellings 
        {
            get => cancelledSellings; 
            set => cancelledSellings = value; 
        }
        public double TotalSoldValue
        { 
            get => totalSoldValue;
            set => totalSoldValue = value;
        }
        public double PixSellings
        {
            get => pixSellings;
            set => pixSellings = value; 
        }
        public double CreditSellings
        { 
            get => creditSellings; 
            set => creditSellings = value; 
        }
        public double DebitSellings
        {
            get => debitSellings;
            set => debitSellings = value;
        }
        public double MoneySellings
        {
            get => moneySellings; 
            set => moneySellings = value;
        }
        
    }
}
