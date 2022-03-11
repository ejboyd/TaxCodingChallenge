namespace CodingChallengeTax
{

    public class TaxCalculation
    {
        private readonly CountyTaxRate _ctr = new CountyTaxRate();
        private readonly decimal subtotal;
        private decimal _taxrate;

        public TaxCalculation(CountyTaxRate? ctr, string str_subtotal)
        {
            this.ValidCalculation = true;
            if (ctr == null)
            {
                this.ValidCalculation = false;
                _ctr = new CountyTaxRate() { CountyName = "Invalid", TaxRate = "0" };
                ValidCalculation = false;
            }
            else
            { 
                _ctr = ctr;
            }

            if (!decimal.TryParse(str_subtotal, out subtotal))
            {
                ValidCalculation = false;
            }
            //set valid true and then check data
            
            ValidateSubtotal();
            ValidateTaxRate();
        }


        public bool ValidCalculation;

        private void ValidateTaxRate()
        {
            decimal val;
            if (!decimal.TryParse(_ctr.TaxRate.Replace("%", ""), out val))
            {
                _taxrate = 0.0M;
                ValidCalculation = false;
            }
            else
            if (_ctr.TaxRate.Contains("%"))
            {
                _taxrate = val / 100;
            }
            else
            {
                _taxrate = val;
            }
            if (_taxrate < 0.0M) ValidCalculation = false;
            return;
        }

        private void ValidateSubtotal()
        {
            const decimal UPPER_BOUND = 100000000M; //setting upper limit to 100 million
            const decimal LOWER_BOUND = 0.0M; //need positve value to calcuate sales tax

            if (subtotal < LOWER_BOUND || subtotal > UPPER_BOUND)
            {
                //value outside of bounds of program
                ValidCalculation = false;
            }
            return;
        }

        private decimal salesTax => subtotal * _taxrate;

        public string TaxRate { get => _taxrate.ToString("P2");  }

        public string CountyName => _ctr.CountyName;
        public string Subtotal => subtotal.ToString("c2");
        public string SalesTax => salesTax.ToString("c2");
        public string Total => (subtotal + salesTax).ToString("c2");

            

    }

}