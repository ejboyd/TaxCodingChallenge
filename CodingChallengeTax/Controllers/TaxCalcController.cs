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
            JsonResult result;
            
            //Match county name to item(CountyTaxRate) in county tax rate data(TaxRates) ignoring case and white space
            CountyTaxRate? selectedCounty = DataManager.GetCountyTaxRate(county);
            
            TaxCalculation taxCalc = new TaxCalculation(selectedCounty, subTotal);
            //everything should be correct here. return calculation
            if (taxCalc.ValidCalculation)
            {
                result = new JsonResult(new { county = taxCalc.CountyName, subTotal = taxCalc.Subtotal, tax = taxCalc.SalesTax, total = taxCalc.Total });
            }
            else
            {
                result = new JsonResult(new { error = "Could not validate the input. Please check and try again." })
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest
                };
            }
            return result;
        }
    }
}