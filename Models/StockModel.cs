using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal class StockModel
    {
        private int id;
        private string name;
        private int startQuantity;
        private int endQuantity;

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
        public int StartQuantity 
        {
            get => startQuantity;
            set => startQuantity = value;
        }
        public int EndQuantity 
        { 
            get => endQuantity; 
            set => endQuantity = value; 
        }
    }
}
