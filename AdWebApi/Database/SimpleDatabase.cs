using AdWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdWebApi.Database
{
    public class SimpleDatabase
    {
        private SimpleDatabase()
        {
        }

        private static SimpleDatabase __database;
        private static List<InventoryData> __inventoryData = new List<InventoryData>();
        private static List<ProductDefinition> __productDefinition = new List<ProductDefinition>();

        public static SimpleDatabase GetInstance()
        {
            return __database ??= new SimpleDatabase();
        }

        public void SaveProductDefinition(ProductDefinition productDefinition)
        {
            ValidateProductDefinition(productDefinition);
            __productDefinition.Add(productDefinition);
        }

        public void SaveProductDefinition(List<ProductDefinition> productDefinition)
        {
            ValidateUniqueProductDefinition(productDefinition);

            foreach (var definition in productDefinition)
                ValidateProductDefinition(definition);

            __productDefinition.AddRange(productDefinition);
        }

        public void SaveInventoryData(InventoryData inventoryData)
        {
            ValidateInventoryData(inventoryData);
            __inventoryData.Add(inventoryData);
        }

        public void SaveInventoryData(List<InventoryData> inventoryData)
        {
            ValidateUniqueInventoryData(inventoryData);

            foreach (var data in inventoryData)
                ValidateInventoryData(data);

            __inventoryData.AddRange(inventoryData);
        }

        public List<InventoryView> GetInventory()
        {
            return (
                from inventoryData in __inventoryData
                from tag in inventoryData.DecodedTags
                select new InventoryView
                {
                    Id = inventoryData.Id,
                    Location = inventoryData.Location,
                    DateOfInventory = inventoryData.DateOfInventory,
                    Filter = tag.Filter,
                    Partition = tag.Partition,
                    CompanyPrefix = tag.CompanyPrefix,
                    ItemReference = tag.ItemReference,
                    Serial = tag.Serial
                }).ToList();
        }

        public void Clear()
        {
            __inventoryData = new List<InventoryData>();
            __productDefinition = new List<ProductDefinition>();
        }

        private void ValidateProductDefinition(ProductDefinition productDefinition)
        {
            if (__productDefinition.Any(x =>
                x.CompanyPrefix == productDefinition.CompanyPrefix &&
                x.ItemReference == productDefinition.ItemReference))
                throw new Exception($"Product Definition with Company Prefix/Item Reference already exist: {productDefinition.CompanyPrefix}/{productDefinition.ItemReference}");
        }

        private void ValidateUniqueProductDefinition(List<ProductDefinition> productDefinitions)
        {
            if (productDefinitions.GroupBy(x => new {x.CompanyPrefix, x.ItemReference}).Any(group => group.Count() > 1))
                throw new Exception("Please check Product Definition for duplicate values.");

        }

        private void ValidateInventoryData(InventoryData inventoryData)
        {
            if(__inventoryData.Any(x=>x.Id == inventoryData.Id && x.Location == inventoryData.Location && x.DateOfInventory == inventoryData.DateOfInventory))
                throw new Exception($"Inventory Data with Id/Location/Date of Inventory already exist: {inventoryData.Id}/{inventoryData.Location}/{inventoryData.DateOfInventory}");
        }

        private void ValidateUniqueInventoryData(List<InventoryData> inventoryData)
        {
            if (inventoryData.GroupBy(x => new { x.Id, x.Location, x.DateOfInventory }).Any(group => group.Count() > 1))
                throw new Exception("Please check Inventory Data for duplicate values.");
        }
    }
}
