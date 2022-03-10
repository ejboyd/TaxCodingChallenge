
using Newtonsoft.Json;

namespace CodingChallengeTax
{
    [JsonObject]
    public class CountyTaxRate
    {
        private string county = "";
        private string taxRate = "";

        [JsonProperty("county")]
        public string CountyName { get => county; set => county = value; }

        [JsonProperty("taxRate")]
        public string TaxRate { get => taxRate; set => taxRate = value; }

    }
}