using System;
using AdWebApi.Database;

namespace AdWebApi.Business.Interfaces
{
    public interface IDataFilter
    {
        int CountByProductAndInventory(string itemReference, string inventoryId);
        int CountByProductPerDay(string itemReference, DateTime date);
        int CountByCompanyPrefix(string companyPrefix);
    }
}
