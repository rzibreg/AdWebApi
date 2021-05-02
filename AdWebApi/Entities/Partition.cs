namespace AdWebApi.Entities
{
    public class Partition
    {
        public static Partition[] GetDefaultPartitionList()
        {
            return new Partition[]
            {
                new() { CompanyPrefixBits = 40, CompanyPrefixDigits = 12, ItemRefBits = 04, ItemRefDigits = 1 },
                new() { CompanyPrefixBits = 37, CompanyPrefixDigits = 11, ItemRefBits = 07, ItemRefDigits = 2 },
                new() { CompanyPrefixBits = 34, CompanyPrefixDigits = 10, ItemRefBits = 10, ItemRefDigits = 3 },
                new() { CompanyPrefixBits = 30, CompanyPrefixDigits = 9, ItemRefBits = 14, ItemRefDigits = 4 },
                new() { CompanyPrefixBits = 27, CompanyPrefixDigits = 8, ItemRefBits = 17, ItemRefDigits = 5 },
                new() { CompanyPrefixBits = 24, CompanyPrefixDigits = 7, ItemRefBits = 20, ItemRefDigits = 6 },
                new() { CompanyPrefixBits = 20, CompanyPrefixDigits = 6, ItemRefBits = 24, ItemRefDigits = 7 }
            };
        }

        /// <summary>
        /// Company prefix bits
        /// </summary>
        public int CompanyPrefixBits { get; set; }

        /// <summary>
        /// Company prefix digits
        /// </summary>
        public int CompanyPrefixDigits { get; set; }

        /// <summary>
        /// Indicator/Item Ref. bits
        /// </summary>
        public int ItemRefBits { get; set; }

        /// <summary>
        /// Indicator/Item Ref. digits
        /// </summary>
        public int ItemRefDigits { get; set; }
    }
}
