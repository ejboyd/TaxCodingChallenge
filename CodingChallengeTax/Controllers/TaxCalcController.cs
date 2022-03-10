using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodingChallengeTax.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalcController : ControllerBase
    {

        private readonly ILogger<TaxCalcController> _logger;

        public TaxCalcController(ILogger<TaxCalcController> logger)
        {
            _logger = logger;
            InitalizeTaxRates();
        }

        //list of tax rate information by county
        List<CountyTaxRate>? TaxRates { get; set; }

        private void InitalizeTaxRates()
        {
            //Read json array of county and tax rates
            using (StreamReader r = new StreamReader(".\\Data\\taxRateNC.json"))
            {
                string json = r.ReadToEnd();
                List<CountyTaxRate>? items = JsonConvert.DeserializeObject<List<CountyTaxRate>>(json);
                if (items != null) { TaxRates = items; } else TaxRates = new List<CountyTaxRate>();
            }

        }

        [HttpGet(Name = "TaxCalc")]
        public JsonResult Get(string county, string subTotal)
        {
            decimal validSubTotal = (decimal)0.0;

            //check input exists
            if (string.IsNullOrWhiteSpace(county) || string.IsNullOrWhiteSpace(subTotal) || !decimal.TryParse(subTotal.Replace("$",""), out validSubTotal))
            {
                var result = new JsonResult(new { error = "calculation requires North Carolina county name and sales subtotal" })
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest
                };
                return result;
            }

            //check server data was initialized
            if (TaxRates == null)
            {
                var result = new JsonResult(new { error = "could not initialize server" })
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                };
                return result;
            }
            else
            {
                //Match county name to item(CountyTaxRate) in county tax rate data(TaxRates) ignoring case and white space
                CountyTaxRate? selectedCounty = TaxRates.FirstOrDefault(taxRate => (string.Compare(taxRate.CountyName,county.Trim(), true) == 0));
                if (selectedCounty == null)
                {
                    string[] validNames = TaxRates.Select(x => x.CountyName).ToArray();

                    //couldnt find name in list of counties. bad county name
                    var result = new JsonResult(new { error = "county name not found in North Carolina data", validCounties = validNames })
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    };
                    return result;
                }
                else
                {
                    TaxCalc taxCalc = new TaxCalc(selectedCounty, validSubTotal); 
                    //everything should be correct here. return calculation
                    return new JsonResult(new { county = taxCalc.CountyName, subTotal = taxCalc.Subtotal, tax = taxCalc.SalesTax, total = taxCalc.Total } );
                }
            }
            
        }
    }
}