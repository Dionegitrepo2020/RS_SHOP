using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class SignupViewModel : AcivityIndicatorHelper
    {
        private readonly SignupService _apiServices = new SignupService();
        public SignupModel SignupModel { get; set; } = new SignupModel();

        private Page _page;

        //Condition
        private string _ConditionId = "0";
        // private string _ConditionId { get; set; }
        public string ConditionId
        {

            get { return _ConditionId; }

            set
            {
                _ConditionId = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dateOfBirth { get; set; }
        public DateTime DateOfBirth
        {

            get { return _dateOfBirth; }

            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public SignupViewModel(Page page)
        {
            _page = page;

            
        }

        public async void SignupAsync()
        {
            if (!ValidationHelper.IsFormValid(SignupModel, _page)) { return; }
            await SignupApiAsync(ConditionId);

        }

        private async Task SignupApiAsync(string ConditionId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IsBusy = true;
                var SignupStatus = await _apiServices.SignUpApiService(SignupModel.FullName,
                           SignupModel.Password, SignupModel.UserEmail, _dateOfBirth.ToString("yyyy-MM-dd"), ConditionId, SignupModel.ParentName, SignupModel.ParentID);
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(SignupStatus);
                var accessToken = jwtDynamic.Value<string>("Message");
                if (!(accessToken == "User SuccessFully Saved."))
                {
                    await PopupNavigation.Instance.PushAsync(new LoginAlert(accessToken));
                }
                else
                {

                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                IsBusy = false;
            }
        }
    }
}
