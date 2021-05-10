using AdWebApi.Entities;
using System;
using System.Collections;
using System.Globalization;

namespace AdWebApi.Business
{
    public static class Sgtin96Decoder
    {
        #region Const

        private const byte BINARY_HEADER = 0x30;
        private const int SUBSTRING_ITEM_REFERENCE = 1;
        private const int FIRST_BIT_HEADER = 0;
        private const int HEADER_BIT_COUNT = 8;
        private const int FIRST_BIT_FILTER = 8;
        private const int FILTER_BIT_COUNT = 3;
        private const int FIRST_BIT_FOR_PARTITION = 11;
        private const int PARTITION_BIT_COUNT = 3;
        private const int FIRST_BIT_SERIAL = 58;
        private const int SERIAL_BIT_COUNT = 38;
        private static readonly Partition[] PartitionValues = Partition.GetDefaultPartitionList();

        #endregion

        public static Sgtin96Tag ConvertFromHex(string hex)
        {
            var bits = HexStringToBitArray(hex);
            VerifyHeaderForSgtin96(bits);

            var tag = new Sgtin96Tag
            {
                Filter = (byte)ReadBytesGetInt(bits, FIRST_BIT_FILTER, FILTER_BIT_COUNT),
                Serial = Convert.ToString(ReadBytesGetLong(bits, FIRST_BIT_SERIAL, SERIAL_BIT_COUNT))
            };

            DecodeByPartition(tag, bits);

            return tag;
        }

        private static BitArray HexStringToBitArray(string tag)
        {
            var bitArray = new BitArray(4 * tag.Length);
            for (var i = 0; i < tag.Length; i++)
            {
                VerifyCharForHex(tag, i);

                var byteInput = byte.Parse(tag[i].ToString(), NumberStyles.HexNumber);
                for (var z = 0; z < 4; z++)
                    bitArray.Set(i * 4 + z, (byteInput & (1 << (3 - z))) != 0);
            }

            return bitArray;
        }

        private static void VerifyCharForHex(string tag, int i)
        {
            if(!Uri.IsHexDigit(tag[i]))
                throw new FormatException($"Invalid hexadecimal format: {tag}");
        }

        private static void VerifyHeaderForSgtin96(BitArray rawBits)
        {
            var header = ReadBytesGetInt(rawBits, FIRST_BIT_HEADER, HEADER_BIT_COUNT);
            if (header != BINARY_HEADER)
                throw new FormatException("Invalid header.");
        }

        private static uint ReadBytesGetInt(BitArray bits, int firstBit, int bitCount)
        {
            if (firstBit < 0 || firstBit + bitCount > bits.Length)
                throw new ArgumentOutOfRangeException(nameof(firstBit));

            uint result = 0;
            for (var i = 0; i < bitCount; i++)
            {
                if (bits[firstBit + bitCount - 1 - i])
                    result |= 1u << i;
            }

            return result;
        }

        private static ulong ReadBytesGetLong(BitArray bits, int firstBit, int bitCount)
        {
            if (firstBit < 0 || firstBit + bitCount > bits.Length)
                throw new ArgumentOutOfRangeException(nameof(firstBit));

            ulong result = 0;
            for (var i = 0; i < bitCount; i++)
            {
                if (bits[firstBit + bitCount - 1 - i])
                    result |= 1ul << i;
            }

            return result;
        }

        private static void DecodeByPartition(Sgtin96Tag tag, BitArray bits)
        {
            var partition = (byte)ReadBytesGetInt(bits, FIRST_BIT_FOR_PARTITION, PARTITION_BIT_COUNT);

            if (PartitionValues.Length < partition || PartitionValues[partition] == null)
                throw new FormatException($"Partition {partition} not defined.");

            var partitionDef = PartitionValues[partition];
            var companyPrefixBits = partitionDef.CompanyPrefixBits;
            var itemRefBits = partitionDef.ItemRefBits;
            var companyPrefixConverted = ReadBytesGetLong(bits, FIRST_BIT_FOR_PARTITION + PARTITION_BIT_COUNT, companyPrefixBits);
            var itemRefConverted = ReadBytesGetLong(bits, FIRST_BIT_FOR_PARTITION + PARTITION_BIT_COUNT + companyPrefixBits, itemRefBits);

            tag.Partition = partition;
            tag.CompanyPrefix = companyPrefixConverted.ToString().PadLeft(partitionDef.CompanyPrefixDigits, '0');
            tag.ItemReference = itemRefConverted.ToString().PadLeft(partitionDef.ItemRefDigits, '0');
        }
    }
}
