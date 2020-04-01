using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.DropdownModel;
using RS_SHOP_Dev.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static RS_SHOP_Dev.Models.DropdownModel.CountyState;

namespace RS_SHOP_Dev.ViewModels
{
    
    public class CountryStateViewModel : AcivityIndicatorHelper
    {
        private readonly LoginSignupService _apiServices = new LoginSignupService();
        public CountyState CountyState { get; set; } = new CountyState();

        //Country/State
        // public ICommand UpdateCountryInCommand { get; }

       

        private Page _page;
        private string userId { get; set; }
        ObservableCollection<Root> _countrylist;
        public ObservableCollection<Root> List
        {
            get
            {
                return _countrylist;
            }
            set
            {
                _countrylist = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Root> _citylist;
        public ObservableCollection<Root> CityList
        {
            get
            {
                return _citylist;
            }
            set
            {
                _citylist = value;
                OnPropertyChanged();
            }
        }

        private string countryID { get; set; }

        private string _countryName { get; set; }
        public string CountryName
        {

            get { return _countryName; }

            set
            {
                _countryName = value;
                OnPropertyChanged();
            }
        }

        private string cityID { get; set; }

        private string _cityName { get; set; }

        public async Task UpdateCountryCity(string countryName, string cityName, string userId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await _apiServices.UpdateCountryAsync(countryName, userId);
                await _apiServices.UpdateCityAsync(cityName, userId);
            }
        }

        public string CityName
        {

            get { return _cityName; }

            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        private DateTime create { get; set; }
        public DateTime Create
        {
            get { return create; }
            set
            {
                create = value;
                OnPropertyChanged();
            }
        }

        private DateTime modify { get; set; }
        public DateTime Modify
        {
            get { return modify; }
            set
            {
                modify = value;
                OnPropertyChanged();
            }
        }

       
        public CountryStateViewModel()
        {
            string UserId = Application.Current.Properties["USER_ID"].ToString();
            LoadCountryList();

         //   UpdateCountryInCommand = new Command(async () => await UpdateCountryAsync(CountryName, UserId));

        }

        private async Task UpdateCountryAsync(string countryName, string userId)
        {
            var UpdateDateStatus = await _apiServices.UpdateCountryAsync(countryName, userId);
            PopupNavigation.Instance.PopAsync();
        }

        public async void LoadCountryList()
        {
            //  IsBusy = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format(Constants.BaseUrl + "country/listallcountry/", string.Empty));
                    var result = await client.GetStringAsync(uri);
                    var countryList = JsonConvert.DeserializeObject<List<Root>>(result);
                    List = new ObservableCollection<Root>(countryList);
                }
            }
        }


        public async void LoadCityList(int item)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format(Constants.BaseUrl + "country/listcountry/" + item + "", string.Empty));
                    var result = await client.GetStringAsync(uri);
                    var citylist = JsonConvert.DeserializeObject<List<Root>>(result);

                    CityList = new ObservableCollection<Root>(citylist);

                }
            }
        }

    }
}
