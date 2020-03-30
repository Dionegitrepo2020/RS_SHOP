using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.Services
{
    public class PayApiService
    {
        public async Task<string> ProcessPayment(JObject jObject)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "pay/add", string.Empty));
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }
    }
}
