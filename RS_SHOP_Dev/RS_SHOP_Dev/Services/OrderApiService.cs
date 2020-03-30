using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.Services
{
    public class OrderApiService
    {
        public async Task<List<OrderResponse>> GenerateFoodDrinkOrderService(JObject jObject)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "oder/add", string.Empty));
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            var client = new HttpClient();
            response = await client.PostAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            List<OrderResponse> jwtDynamic = JsonConvert.DeserializeObject<List<OrderResponse>>(contents);
            //var accessToken = jwtDynamic.Value<string>("ORDER_QR_TAG");
            return jwtDynamic;
        }

        public async Task<List<OrderModel>> GetFromOrderService(string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "oder/listall/" + userId, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var ProdList = JsonConvert.DeserializeObject<List<OrderModel>>(result);
            return ProdList;
        }

        public async Task<List<OrderModel>> GetOrderByIdService(string order_Id)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "oder/list/" + order_Id, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var ProdList = JsonConvert.DeserializeObject<List<OrderModel>>(result);
            return ProdList;
        }
    }
}
