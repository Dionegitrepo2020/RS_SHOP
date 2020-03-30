using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.CartModels;
using RS_SHOP_Dev.Models.ShoppingModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.Services
{
    public class CartApiService
    {
        public async Task<string> AddtocartService(TB_ECOMM_CART_ITEM tB_ECOMM_CART_ITEM)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/addtocart", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_ID", tB_ECOMM_CART_ITEM.USER_ID);
            jObject.Add("PRODUCT_ID", tB_ECOMM_CART_ITEM.PRODUCT_ID);
            jObject.Add("CART_ITEM_QUANTITY", tB_ECOMM_CART_ITEM.CART_ITEM_QUANTITY);
            jObject.Add("CATEGORY_ID", tB_ECOMM_CART_ITEM.CATEGORY_ID);
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            var accessToken = jwtDynamic.Value<string>("Status");
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            return contents;
        }

        public async Task<List<CartProducts>> GetFromcartService(string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/listcart/"+ userId, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var ProdList = JsonConvert.DeserializeObject<List<CartProducts>>(result);
            return ProdList;
        }

        public async Task<List<CartProducts>> GetMerchFromcartService(string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/listcartM/" + userId, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var ProdList = JsonConvert.DeserializeObject<List<CartProducts>>(result);
            return ProdList;
        }

        public async Task UpdateCartService(string cart_Id, string cart_Qty)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/updatecart", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("CART_ITEM_ID", cart_Id);
            jObject.Add("CART_ITEM_QUANTITY", cart_Qty);
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var result = await client.PutAsync(uri,content);
        }

        public async Task DeleteCartService(int cart_ID)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/removefromcart/" + cart_ID, string.Empty));
            var client = new HttpClient();
            var result = await client.DeleteAsync(uri);
        }

        public async Task DeleteAllCartService(string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "item/removeallfromcart/" + userId, string.Empty));
            var client = new HttpClient();
            var result = await client.DeleteAsync(uri);
        }
    }
}
