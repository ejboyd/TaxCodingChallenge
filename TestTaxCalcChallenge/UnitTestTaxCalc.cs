using CodingChallengeTax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestTaxCalcChallenge
{
    [TestClass]
    public class UnitTestTaxCalc
    {
        CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "7%" };


        [TestMethod]
        //Test that rounding down works correctly to 2 decimal places
        public void TestMethod_RoundingInputDown()
        {
            TaxCalc taxCalc = new TaxCalc(test_Madison , (decimal)501.44499999);
            Assert.IsTrue(taxCalc.Subtotal == "$501.44");
        }

        [TestMethod]
        //Test that rounding up works correctly to 2 decimal places
        public void TestMethod_RoundingInputUp()
        {
            TaxCalc taxCalc = new TaxCalc(test_Madison, (decimal)501.445000000);
            Assert.IsTrue(taxCalc.Subtotal == "$501.45");
        }

       
        [TestMethod]
        //Test tax rate input with percent sign
        public void TestMethod_ParseTaxRatePercentSign()
        {
            decimal testValue = TaxCalc.TaxRateFloat("75.75%");

            Assert.IsTrue(testValue == .7575M);
        }


        [TestMethod]
        //test tax rate input with space and percent
        public void TestMethod_ParseTaxRatePercentSignSpace()
        {
            decimal testValue = TaxCalc.TaxRateFloat("75.75 %");

            Assert.IsTrue(testValue == .7575M);
        }


        [TestMethod]
        //tax rate input with out percent sign
        public void TestMethod_ParseTaxRateNoPercentSign()
        {
            decimal testValue = TaxCalc.TaxRateFloat(".7575");

            Assert.IsTrue(testValue == .7575M);
        }

        [TestMethod]
        //test the view of the tax rate in results with percent
        public void TestMethod_OutputTaxratePercent()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "Percent", TaxRate = "7%" };
            TaxCalc taxCalc = new TaxCalc(test_Percent, 501.50M);
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "7.00%"));
        }

        [TestMethod]
        //test the view of the tax rate with decimal and percent
        public void TestMethod_OutputTaxratePercentDecimal()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "PercentDecimal", TaxRate = "7.00%" };
            TaxCalc taxCalc = new TaxCalc(test_Percent, 501.50M);
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "7.00%"));
        }

        [TestMethod] //test the view of the tax rate in results without percent
        public void TestMethod_OutputTaxrateNoPercent()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "NoPercent", TaxRate = ".07" };
            TaxCalc taxCalc = new TaxCalc(test_Percent, 501.50M);
            Assert.IsTrue(string.Equals(taxCalc.TaxRate,"7.00%"));
        }

    }
}