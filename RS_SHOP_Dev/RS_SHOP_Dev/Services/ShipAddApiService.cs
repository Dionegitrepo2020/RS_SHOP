using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.ShipAddressModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RS_SHOP_Dev.Services
{
    public class ShipAddApiService
    {
        public async Task<string> SaveShipAddAsync(ShipAddressModel shipAddressModel)
        {
           
            //api/shipaddress/add
            var uri = new Uri(string.Format(Constants.BaseUrl + "shipaddress/add", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_ID", Application.Current.Properties["USER_ID"].ToString());
            jObject.Add("FULL_NAME", shipAddressModel.FULL_NAME);
            jObject.Add("ADDRESS1", shipAddressModel.ADDRESS1);
            jObject.Add("ADDRESS2", shipAddressModel.ADDRESS2);
            jObject.Add("COUNTRY", shipAddressModel.COUNTRY);
            jObject.Add("POST_CODE", shipAddressModel.POST_CODE);
            jObject.Add("CITY", shipAddressModel.CITY);
            jObject.Add("PHONE", shipAddressModel.PHONE);
            jObject.Add("IS_DEFAULT", shipAddressModel.IS_DEFAULT);
            
           
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            var accessToken = jwtDynamic.Value<string>("Message");
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            return contents;
        }


        public async Task<List<ShipAddressModel>> LoadShipAddAsync(string userId)
        {
            //shipaddress/listall/UID
            var uri = new Uri(string.Format(Constants.BaseUrl + "shipaddress/listall/" + userId, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            List<ShipAddressModel> ShipAddList = JsonConvert.DeserializeObject<List<ShipAddressModel>>(result);
            return ShipAddList;
        }

        public async Task<List<ShipAddressModel>> RemoveAddressAsync(int address_ID)
        {
            //shipaddress/removeaddress/CID
            var uri = new Uri(string.Format(Constants.BaseUrl + "shipaddress/removeaddress/" + address_ID, string.Empty));
            var client = new HttpClient();
            var result = await client.DeleteAsync(uri);

            List<ShipAddressModel> ShipAddList = await LoadShipAddAsync(Application.Current.Properties["USER_ID"].ToString());
            return ShipAddList;
        }

    }
}
