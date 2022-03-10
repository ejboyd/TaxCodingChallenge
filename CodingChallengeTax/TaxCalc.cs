namespace CodingChallengeTax
{

    public class TaxCalc
    {
        private readonly CountyTaxRate ctr;
        private readonly decimal subtotal;

        public TaxCalc(CountyTaxRate ctr, decimal subtotal)
        {
            this.ctr = ctr;
            this.subtotal = subtotal;
        }

        private decimal salesTax => subtotal * TaxRateFloat(ctr.TaxRate);

        public string TaxRate { get => TaxRateFloat(ctr.TaxRate).ToString("P2");  }

        public static decimal TaxRateFloat(string taxRate)
        {
            decimal val;

            if (!decimal.TryParse(taxRate.Replace("%", ""), out val))
            {
                throw new Exception(string.Format("Can not parse input as tax rate {0}", taxRate));
            }

            if (taxRate.Contains("%"))
            {
                return val / 100;
            }
            else
            {
                return val;
            }
        }

        public string CountyName => ctr.CountyName;
        public string Subtotal => subtotal.ToString("c2");
        public string SalesTax => salesTax.ToString("c2");
        public string Total => (subtotal + salesTax).ToString("c2");

    }

}