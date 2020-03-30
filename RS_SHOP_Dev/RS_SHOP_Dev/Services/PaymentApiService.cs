using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.PaymentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RS_SHOP_Dev.Services
{
    public class PaymentApiService
    {
      
        public async Task<string> SaveCardAsync(PaymentListModel paymentListModel)
        {
           
            var uri = new Uri(string.Format(Constants.BaseUrl + "payment/add", string.Empty));
            JObject jObject = new JObject();
             jObject.Add("USER_ID", Application.Current.Properties["USER_ID"].ToString());
            jObject.Add("CARD_EXP_DATE", paymentListModel.CARD_EXP_DATE);
            jObject.Add("CARD_HOLDER_NAME", paymentListModel.CARD_HOLDER_NAME);
            jObject.Add("CARD_NUMBER", paymentListModel.CARD_NUMBER.ToString());
            jObject.Add("CARD_TYPE", paymentListModel.CARD_TYPE);

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

        public async Task<List<PaymentListModel>> LoadCardAsync(string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "payment/listall/"+userId, string.Empty));
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var CardList = JsonConvert.DeserializeObject<List<PaymentListModel>>(result);
            return CardList;
        }

        public async Task<List<PaymentListModel>> RemoveCardAsync(int cARD_ID)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "payment/removecard/" + cARD_ID, string.Empty));
            var client = new HttpClient();
            var result = await client.DeleteAsync(uri);

            List<PaymentListModel> CardList=await LoadCardAsync(Application.Current.Properties["USER_ID"].ToString());
            return CardList;
        }
    }
}
