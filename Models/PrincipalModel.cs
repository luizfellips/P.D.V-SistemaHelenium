using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    public class PrincipalModel
    {
        private int id;
        private string name;
        private int quantity;
        private double price;
        private double total;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
