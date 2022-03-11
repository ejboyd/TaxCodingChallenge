using CodingchallengeTax;
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
        }

        private bool ValidateCountyName(string countyName)
        {
            //check if empty
            //could check that values are (A-Z)
            return !string.IsNullOrWhiteSpace(countyName);
        }

        private bool ValidateSubTotal(string input, out decimal m_subtotal)
        {
            return decimal.TryParse(input, out m_subtotal);

        }


        [HttpGet(Name = "TaxCalc")]
        public JsonResult Get(string county, string subTotal)
        {
            decimal validSubTotal;

            JsonResult result;
            
            if (ValidateSubTotal(subTotal, out validSubTotal) == false || ValidateCountyName(county) == false)
            {
                // validate didnt' work
                result = new JsonResult(new { error = "Input not recognized as valid. Please check values and try again" })
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest
                };
            }
            else
            {
                //Match county name to item(CountyTaxRate) in county tax rate data(TaxRates) ignoring case and white space
                CountyTaxRate? selectedCounty = DataManager.GetCountyTaxRate(county);
                if (selectedCounty == null)
                {
                    //couldnt find name in list of counties. bad county name
                    result = new JsonResult(new { error = "County name not found in Data" })
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.BadRequest
                    };
                }
                else
                {
                    TaxCalc taxCalc = new TaxCalc(selectedCounty, validSubTotal);
                    //everything should be correct here. return calculation
                    result = new JsonResult(new { county = taxCalc.CountyName, subTotal = taxCalc.Subtotal, tax = taxCalc.SalesTax, total = taxCalc.Total });
                }

            }
            return result;
        }
    }
}