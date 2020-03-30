using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.Services
{
    public class LoginSignupService: AcivityIndicatorHelper
    {
        public async Task<string> LoginAsync(string userName, string password)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/Login", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_EMAIL", userName);
            jObject.Add("User_PASSWORD", password);
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


       
        //Signup Changes
        public async Task<string> RegistrationAsync(string fullName, string password,
                                                    string email)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/signup", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_NAME", fullName);
            jObject.Add("User_PASSWORD", password);
            jObject.Add("USER_EMAIL", email);
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

        //Signup Modification
        public async Task<string> RegistrationAsync1(string fullName, string password, 
                                                    string email, string dateOfBirth, string conditionId, 
                                                    string parentname, string parentId)
        {
           // var dt = Convert.ToDateTime(dateOfBirth).ToString("yyyy-MM-dd");
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/signup", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("USER_NAME", fullName);
            jObject.Add("User_PASSWORD", password);
            jObject.Add("USER_EMAIL", email);
          //  jObject.Add("DATE_OF_BIRTH", dateOfBirth);
            jObject.Add("DATE_OF_BIRTH", Convert.ToDateTime(dateOfBirth).ToString("yyyy-MM-dd"));
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

        
        //Forgot Password

        public async Task<string> ForgotAsync(ForgotPasswordModel forgotPasswordModel)
        {
            //Constants.BaseUrl + "User/signup", string.Empty
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/forgotpass", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("email", forgotPasswordModel.Email);
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
            }
            return contents;
        }


        // Update Contact Name
        public async Task<string> UpdateContactAsync(ProfileModel profileModel, string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/USER_NAME/"+ profileModel.FirstName+ profileModel.LastName + "/"+ userId + "", string.Empty));
            
            JObject jObject = new JObject();
          //  jObject.Add("USER_NAME", profileModel.FirstName + profileModel.LastName);
           
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            // response = await client.PostAsync(uri, content);
            response = await client.PutAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            //JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            return contents;
        }


        // Update Date
        public async Task<string> UpdateDateAsync(string dob, string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/DATE_OF_BIRTH/"+ dob + "/" + userId + "", string.Empty));

            JObject jObject = new JObject();
          //  jObject.Add("DATE_OF_BIRTH", profileModel.Date);

            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            // response = await client.PostAsync(uri, content);
            response = await client.PutAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            //JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            return contents;
        }

        //Update Gender
        public async Task<string> UpdateGenderAsync(string item, string userId)
        {

            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/GENDER/" + item + "/" + userId + "", string.Empty));

            JObject jObject = new JObject();
            //  jObject.Add("DATE_OF_BIRTH", profileModel.Date);

            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            // response = await client.PostAsync(uri, content);
            response = await client.PutAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            //JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            return contents;
        }

        //Update Photo

        /*public async Task<string> UpdatePhotoAsync(string imagePath, string userId)
        {
            //  var value = "USER_PROFILE_IMAGE";

            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/USER_PROFILE_IMAGE/" + imagePath  + "/" + userId + "", string.Empty));

          //  var uri = new Uri(string.Format("http://dionesql.southindia.cloudapp.azure.com/Rsshop1/" + imagePath + "/" + userId + "", string.Empty));

            JObject jObject = new JObject();
            //  jObject.Add("DATE_OF_BIRTH", profileModel.Date);

            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            // response = await client.PostAsync(uri, content);
            response = await client.PutAsync(uri, content);

            var contents = await response.Content.ReadAsStringAsync();
            //JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            return contents;
        }*/


        //Update Country
        public async Task<string> UpdateCountryAsync(string countryName, string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/COUNTRY/" + countryName + "/" + userId + "", string.Empty));

            JObject jObject = new JObject();
           
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;
            
            response = await client.PutAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }

        //City
        public async Task<string> UpdateCityAsync(string cityName, string userId)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "User/update/CITY/" + cityName + "/" + userId + "", string.Empty));

            JObject jObject = new JObject();

            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }

        public async Task<string> PasswordResetAsync(PasswordResetModel passwordResetModel)
        {
            var uri = new Uri(string.Format(Constants.BaseUrl + "user/updatepass", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("NewPassword", passwordResetModel.Password);
            jObject.Add("ConfirmPassword", passwordResetModel.ConfirmPassword);
            jObject.Add("ResetCode", passwordResetModel.ActivationCode);
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }

        public async Task<string> ActivateAccount(string email)
        {
            IsBusy = true;
            var uri = new Uri(string.Format(Constants.BaseUrl + "user/activateaccount", string.Empty));
            JObject jObject = new JObject();
            jObject.Add("Email", email);
            var json = JsonConvert.SerializeObject(jObject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);
            var contents = await response.Content.ReadAsStringAsync();
            IsBusy = false;
            return contents;
        }
    }
}
