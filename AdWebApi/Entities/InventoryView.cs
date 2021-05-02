using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdWebApi.Entities
{
    public class InventoryView : Sgtin96Tag
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public DateTime DateOfInventory { get; set; }
    }
}
