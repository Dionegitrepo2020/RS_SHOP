using Newtonsoft.Json;
using RS_SHOP_Dev.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace RS_SHOP_Dev.ViewModels
{
    public class DropdownViewModel : AcivityIndicatorHelper
    {
        private string userId { get; set; }
      //  public List<City> CitiesList { get; set; }
      //  public List<Country> CountryList { get; set; }

        ObservableCollection<City> _countrylist;
        public ObservableCollection<City> CountryList
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

        ObservableCollection<City> _citylist;
        public ObservableCollection<City> CityList
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


       /* ObservableCollection<RootObject> _list;
        public ObservableCollection<RootObject> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }*/

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


        public DropdownViewModel()
        {
            LoadCountryList();
        }

        public async void LoadCountryList()
        {
          //  IsBusy = true;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "country/listallcountry/", string.Empty));
                var result = await client.GetStringAsync(uri);
                var countryList = JsonConvert.DeserializeObject<List<City>>(result);

                CountryList = new ObservableCollection<City>(countryList);
               
            }
        }


        public async void LoadCityList(int item)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "country/listcountry/" + item + "", string.Empty));
                var result = await client.GetStringAsync(uri);
                var citylist = JsonConvert.DeserializeObject<List<City>>(result);

                CityList = new ObservableCollection<City>(citylist);

            }
        }

    }


    public class Country
    {
        public int COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    /*public class City
    {
        public int CITY_ID { get; set; }
        public int COUNTRY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }

    }*/

   
    public class City
    {
       
       /* public object country { get; set; }
        public City city { get; set; }*/

          public Country country { get; set; }
          public object city { get; set; }
    }


    //Called this method in EditCountryPop
    /* private void Picker_SelectedIndexChanged(object sender, EventArgs e)
         {
             Picker picker = sender as Picker;
             City selectedItem = (City)picker.SelectedItem;
             var item = selectedItem.country.COUNTRY_ID;
             (this.BindingContext as DropdownViewModel).LoadCityList(item);
             //  DisplayAlert("", selectedItem,"Ok");
         }*/
}
