using AdWebApi.Business;
using AdWebApi.Database;
using AdWebApi.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdWebApiUnitTests.Business
{
    [TestClass]
    public class DataFilterTests
    {
        private SimpleDatabase __database;
        private DataFilter __dataFilter;
        
        [TestMethod]
        [TestCategory("DataFilter")]
        public void CountByProductAndInventory_returns_expected_value()
        {
            //30D4068B5B0A54C00D853004
            var actualCount = __dataFilter.CountByProductAndInventory("797011", "STOCK005");

            Assert.AreEqual(2, actualCount);
        }

        [TestMethod]
        [TestCategory("DataFilter")]
        public void CountByProductPerDay_returns_expected_value()
        {
            //30727B26B233998003303C79
            var actualCount = __dataFilter.CountByProductPerDay("52838", new DateTime(2021, 5, 2));

            Assert.AreEqual(2, actualCount);
        }

        [TestMethod]
        [TestCategory("DataFilter")]
        public void CountByCompanyPrefix_returns_expected_value()
        {
            //3098D0A357783C0034E9DF74
            var actualCount = __dataFilter.CountByCompanyPrefix("213645");

            Assert.AreEqual(5, actualCount);
        }

        [TestInitialize]
        public void Setup()
        {
            __database = SimpleDatabase.GetInstance();
            __dataFilter = new DataFilter();
            __database.SaveProductDefinition(new List<ProductDefinition>()
            {
                new ()
                {
                    CompanyPrefix = 213645,
                    CompanyName = "Sanford LLC",
                    ItemReference = 6152432,
                    ItemName = "Beans - Kidney, Red Dry"
                },
                new ()
                {
                    CompanyPrefix = 719065,
                    CompanyName = "Gleichner, Rodriguez and Wilkinson",
                    ItemReference = 9765179,
                    ItemName = "Scallops - In Shell"
                },
                new ()
                {
                    CompanyPrefix = 0107222,
                    CompanyName = "Torphy-Becker",
                    ItemReference = 797011,
                    ItemName = "Wine - Merlot Vina Carmen"
                },
                new ()
                {
                    CompanyPrefix = 83250532,
                    CompanyName = "McGlynn Inc",
                    ItemReference = 52838,
                    ItemName = "Pickles - Gherkins"
                }
            });

            var inventoryData = new List<InventoryData>
            {
                new ()
                {
                    Id = "STOCK001",
                    Location = "Zagreb",
                    DateOfInventory = new DateTime(2021,5,1),
                    Tags = new List<string>()
                    {
                        "3098D0A357783C0034E9DF74",
                        "307ABE3665404EC00F863485",
                        "309B1BC6D36B93C032AAD59A"
                    }
                },
                new ()
                {
                    Id = "STOCK002",
                    Location = "Split",
                    DateOfInventory = new DateTime(2021,5,2),
                    Tags = new List<string>()
                    {
                        "3098D0A357783C0034E9DF74",
                        "307ABE3665404EC00F863485",
                        "309B1BC6D36B93C032AAD59A",
                        "30727B26B233998003303C79"
                    }
                },
                new ()
                {
                    Id = "STOCK003",
                    Location = "Varaždin",
                    DateOfInventory = new DateTime(2021,5,3),
                    Tags = new List<string>()
                    {
                        "3098D0A357783C0034E9DF74",
                        "307ABE3665404EC00F863485",
                        "3098D0A357783C0034E9DF74",
                        "307ABE3665404EC00F863485"
                    }
                },
                new ()
                {
                    Id = "STOCK004",
                    Location = "Osijek",
                    DateOfInventory = new DateTime(2021,5,2),
                    Tags = new List<string>()
                    {
                        "3078325EA2A9DB8003A7CACC",
                        "30DAE4D248EA4F4039309983",
                        "3078325EA2A9DB8003A7CACC"
                    }
                },
                new ()
                {
                    Id = "STOCK005",
                    Location = "Rijeka",
                    DateOfInventory = new DateTime(2021,5,2),
                    Tags = new List<string>()
                    {
                        "30727B26B233998003303C79",
                        "305B747BA582600033AF5994",
                        "30D4068B5B0A54C00D853004",
                        "30D4068B5B0A54C00D853004",
                        "3075E4C70E27E180119FA3FC"
                    }
                },
                new ()
                {
                    Id = "STOCK006",
                    Location = "Virovitica",
                    DateOfInventory = new DateTime(2021,5,1),
                    Tags = new List<string>()
                    {
                        "3098D0A357783C0034E9DF74",
                        "307ABE3665404EC00F863485",
                        "309B1BC6D36B93C032AAD59A",
                        "30727B26B233998003303C79",
                        "305B747BA582600033AF5994",
                        "30D4068B5B0A54C00D853004",
                        "3075E4C70E27E180119FA3FC",
                        "3078325EA2A9DB8003A7CACC",
                        "30DAE4D248EA4F4039309983"
                    }

                }
            };

            foreach (var inventory in inventoryData)
                inventory.DecodedTags = inventory.Tags.Select(Sgtin96Decoder.ConvertFromHex).ToList();

            __database.SaveInventoryData(inventoryData);
        }

        [TestCleanup]
        public void Cleanup()
        {
            __database.Clear();
        }
    }
}
