using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.Services
{
    public class SignupService
    {
       
        public async Task<string> SignUpApiService(string fullName, string password,
                                                   string email, string dateOfBirth, string conditionId,
                                                   string parentname, string parentId)
        {
            // var dt = Convert.ToDateTime(dateOfBirth).ToString("yyyy-MM-dd");
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/signup", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_NAME", fullName);
            jObject.Add("User_PASSWORD", password);
            jObject.Add("USER_EMAIL", email);
            jObject.Add("DATE_OF_BIRTH", Convert.ToDateTime(dateOfBirth).ToString("yyyy-MM-dd"));
            //  jObject.Add("DATE_OF_BIRTH", Convert.ToDateTime(dateOfBirth).ToString("yyyy-MM-dd HH:mm:ss"));
            jObject.Add("CONDITION_ID", conditionId);
            jObject.Add("PARENT_NAME", parentname);
            jObject.Add("PARENT_ID", parentId);
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

    }
}
