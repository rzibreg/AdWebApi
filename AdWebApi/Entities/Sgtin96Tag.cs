using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdWebApi.Entities
{
    public class Sgtin96Tag
    {
        public byte Filter { get; set; }
        public byte Partition { get; set; }
        public string CompanyPrefix { get; set; }
        public string ItemReference { get; set; }
        public string Serial { get; set; }
    }
}
