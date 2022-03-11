using CodingChallengeTax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestTaxCalcChallenge
{
    [TestClass]
    public class UnitTestTaxCalc
    {
        //CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "7%" };


        [TestMethod]
        //Test that rounding down works correctly to 2 decimal places
        public void TestMethod_RoundingInputDown()
        {
            CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "7%" };
            TaxCalculation taxCalc = new TaxCalculation(test_Madison , "501.44499999");
            Assert.IsTrue(string.Equals(taxCalc.Subtotal, "$501.44"));
        }

        [TestMethod]
        //Test that rounding up works correctly to 2 decimal places
        public void TestMethod_RoundingInputUp()
        {
            CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "7%" };
            TaxCalculation taxCalc = new TaxCalculation(test_Madison, "501.445000000");
            Assert.IsTrue(string.Equals(taxCalc.Subtotal,"$501.45"));
        }

       
        [TestMethod]
        //Test tax rate input with percent sign
        public void TestMethod_ParseTaxRatePercentSign()
        {
            CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "75.75%" };
            TaxCalculation taxCalc = new TaxCalculation(test_Madison, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "75.75%"));
        }


        [TestMethod]
        //test tax rate input with space and percent
        public void TestMethod_ParseTaxRatePercentSignSpace()
        {
            CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = "75.75 %" };
            TaxCalculation taxCalc = new TaxCalculation(test_Madison, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "75.75%"));
        }


        [TestMethod]
        //tax rate input with out percent sign
        public void TestMethod_ParseTaxRateNoPercentSign()
        {
            CountyTaxRate test_Madison = new CountyTaxRate() { CountyName = "MadisonTest", TaxRate = ".7575" };
            TaxCalculation taxCalc = new TaxCalculation(test_Madison, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate,"75.75%"));
        }

        [TestMethod]
        //test the view of the tax rate in results with percent
        public void TestMethod_OutputTaxratePercent()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "Percent", TaxRate = "7%" };
            TaxCalculation taxCalc = new TaxCalculation(test_Percent, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "7.00%"));
        }

        [TestMethod]
        //test the view of the tax rate with decimal and percent
        public void TestMethod_OutputTaxratePercentDecimal()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "PercentDecimal", TaxRate = "7.00%" };
            TaxCalculation taxCalc = new TaxCalculation(test_Percent, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate, "7.00%"));
        }

        [TestMethod] //test the view of the tax rate in results without percent
        public void TestMethod_OutputTaxrateNoPercent()
        {
            CountyTaxRate test_Percent = new CountyTaxRate() { CountyName = "NoPercent", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Percent, "501.50");
            Assert.IsTrue(string.Equals(taxCalc.TaxRate,"7.00%"));
        }
       
        [TestMethod]
        public void TestMethod_OutputTotal()
        {
            CountyTaxRate test_Total = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Total, "5.0");
            Assert.IsTrue(string.Equals(taxCalc.Total, "$5.35"));
        }

        [TestMethod]
        public void TestMethod_OutputTax()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax, "5.0");
            Assert.IsTrue(string.Equals(taxCalc.SalesTax, "$0.35"));
        }

        [TestMethod]
        public void TestMethod_InputNegative()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax,"-0.01");
            Assert.IsTrue(!taxCalc.ValidCalculation);
        }
        
        [TestMethod]
        public void TestMethod_InputOver()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax, "100000000001.00");
            Assert.IsTrue(!taxCalc.ValidCalculation);
        }

        [TestMethod]
        public void TestMethod_TaxRateNegative()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = "-.07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax, "10.0");
            Assert.IsTrue(!taxCalc.ValidCalculation);
        }

        [TestMethod]
        public void TestMethod_TaxRateZero()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = "0.0" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax, "10.0");
            Assert.IsTrue(taxCalc.ValidCalculation);
        }


        [TestMethod]
        public void TestMethod_TaxRatePostive()
        {
            CountyTaxRate test_Tax = new CountyTaxRate() { CountyName = "TotalTest", TaxRate = ".07" };
            TaxCalculation taxCalc = new TaxCalculation(test_Tax, "10.0");
            Assert.IsTrue(taxCalc.ValidCalculation);
        }

    }
}