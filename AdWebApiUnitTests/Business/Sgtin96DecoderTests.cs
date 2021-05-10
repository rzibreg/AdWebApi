using AdWebApi.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using AdWebApi.Entities;

namespace AdWebApiUnitTests.Business
{
    [TestClass]
    public class Sgtin96DecoderTests
    {
        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_zero()
        {
            var arrangedHex = "30822F962F574C8035C0C75B";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("600851600851", converterResult.CompanyPrefix);
            Assert.AreEqual("2", converterResult.ItemReference);
            Assert.AreEqual(0, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_one()
        {
            var arrangedHex = "3024DFD31D1DE0C000000001";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("30041237743", converterResult.CompanyPrefix);
            Assert.AreEqual("03", converterResult.ItemReference);
            Assert.AreEqual(1, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_two()
        {
            var arrangedHex = "3089F8221B8D6B800E501E97";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("8457952141", converterResult.CompanyPrefix);
            Assert.AreEqual("430", converterResult.ItemReference);
            Assert.AreEqual(2, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_three()
        {
            var arrangedHex = "302D28B329B0F6C000000001";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("311112347", converterResult.CompanyPrefix);
            Assert.AreEqual("0987", converterResult.ItemReference);
            Assert.AreEqual(3, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_four()
        {
            var arrangedHex = "30318641574C7EC02B1E71DD";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("51151534", converterResult.CompanyPrefix);
            Assert.AreEqual("78331", converterResult.ItemReference);
            Assert.AreEqual(4, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_five()
        {
            var arrangedHex = "30D6431BEC905740056E9860";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("9488123", converterResult.CompanyPrefix);
            Assert.AreEqual("147805", converterResult.ItemReference);
            Assert.AreEqual(5, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_five_test()
        {
            var arrangedHex = "3074257bf7194e4000001a85";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("0614141", converterResult.CompanyPrefix);
            Assert.AreEqual("812345", converterResult.ItemReference);
            Assert.AreEqual(5, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        public void ConvertFromHex_returns_correct_data_for_valid_sgtin96_tag_with_partition_six()
        {
            var arrangedHex = "309BA00B0915150001BCA55E";
            var converterResult = Sgtin96Decoder.ConvertFromHex(arrangedHex);

            Assert.AreEqual("950316", converterResult.CompanyPrefix);
            Assert.AreEqual("2380884", converterResult.ItemReference);
            Assert.AreEqual(6, converterResult.Partition);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        [ExpectedException(typeof(FormatException), "Invalid header.")]
        public void ConvertFromHex_throws_exception_for_invalid_sgtin96_header_tag()
        {
            var arrangedHex = "385B26AA444FE94027CE82A8";
            Sgtin96Decoder.ConvertFromHex(arrangedHex);
        }

        [TestMethod]
        [TestCategory("Sgtin96Decoder")]
        [ExpectedException(typeof(FormatException), "Invalid hexadecimal format: 30HB747BA582600005AE9068")]
        public void ConvertFromHex_throws_exception_for_invalid_hexadecimal_tag()
        {
            var arrangedHex = "30HB747BA582600005AE9068";
            Sgtin96Decoder.ConvertFromHex(arrangedHex);
        }
    }
}
