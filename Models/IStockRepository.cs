using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockController.Models
{
    internal interface IStockRepository
    {
        void FirstInitializationAdd(int id, string name, int quantity);
        void UpdateInfos(int id, int quantity);
        List<StockModel> GetAll();
        void ClearDayStockControl();
    }
}
