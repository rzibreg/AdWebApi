using AdWebApi.Business.Interfaces;
using AdWebApi.Database;
using System;
using System.Linq;

namespace AdWebApi.Business
{
    public class DataFilter : IDataFilter
    {
        private readonly SimpleDatabase __database;

        public DataFilter()
        {
            __database = SimpleDatabase.GetInstance();
        }

        public int CountByProductAndInventory(string itemReference, string inventoryId)
        {
            return __database.GetInventory().Count(x => x.ItemReference == itemReference && x.Id == inventoryId);
        }

        public int CountByProductPerDay(string itemReference, DateTime date)
        {
            return __database.GetInventory().Count(x => x.ItemReference == itemReference && x.DateOfInventory.Date == date.Date);
        }

        public int CountByCompanyPrefix(string companyPrefix)
        {
            return __database.GetInventory().Count(x => x.CompanyPrefix == companyPrefix);
        }
    }
}
