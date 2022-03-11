using CodingChallengeTax;
using Newtonsoft.Json;

namespace CodingchallengeTax
{
    public class DataManager
    {
        //list of tax rate information by county

        static List<CountyTaxRate>? _taxrates;

        static List<CountyTaxRate> TaxRates
        {
            get
            {
                if (_taxrates == null)
                {
                    //Read json array of county and tax rates
                    using (StreamReader r = new StreamReader(".\\Data\\taxRateNC.json"))
                    {
                        string json = r.ReadToEnd();
                        List<CountyTaxRate>? items = JsonConvert.DeserializeObject<List<CountyTaxRate>>(json);
                        if (items != null) { _taxrates = items; } else _taxrates = new List<CountyTaxRate>();
                    }
                }
                return _taxrates;
            }
        }


        public static CountyTaxRate? GetCountyTaxRate(string countyName)
        {
            if (!string.IsNullOrEmpty(countyName))
            {
                return TaxRates.FirstOrDefault(taxRate => taxRate.CountyName.Equals(countyName));
            }
            else return null;
        }

    }
}
