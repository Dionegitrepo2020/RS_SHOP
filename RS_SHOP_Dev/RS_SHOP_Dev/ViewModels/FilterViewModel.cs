using Newtonsoft.Json;
using Plugin.Connectivity;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.SearchFilterModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class FilterViewModel : AcivityIndicatorHelper
    {
        public ICommand ApplyFilter { get; }

        ObservableCollection<FilterModel> _filterData;
        public ObservableCollection<FilterModel> FilterData
        {
            get
            {
                return _filterData;
            }
            set
            {
                _filterData = value;
                OnPropertyChanged();
            }
        }

        long _maxPrice;
        public long MaxPrice
        {
            get
            {
                return _maxPrice;
            }
            set
            {
                _maxPrice = value;
                OnPropertyChanged("MaxPrice");
            }
        }
        long _upperPrice;
        public long UpperPrice
        {
            get
            {
                return _upperPrice;
            }
            set
            {
                _upperPrice = value;
                OnPropertyChanged("UpperPrice");
            }
        }
        long _lowerPrice;
        public long LowerPrice
        {
            get
            {
                return _lowerPrice;
            }
            set
            {
                _lowerPrice = value;
                OnPropertyChanged("LowerPrice");
            }
        }
        FilterModel _subcateg;
        public FilterModel subcateg
        {
            get
            {
                return _subcateg;
            }
            set
            {
                _subcateg = value;
                OnPropertyChanged("subcateg");
            }
        }
        public FilterViewModel()
        {
            ApplyFilter = new Command(async () => await ApplyFilterAsync());
        }

        private async Task ApplyFilterAsync()
        {
            var Min_price = LowerPrice.ToString()??"1";
            var Max_price = UpperPrice.ToString()??"0";
            var Sub_cat = subcateg?.subcategid.ToString()??"0";
            string[] values = { Min_price, Max_price, Sub_cat };
            MessagingCenter.Send<App, string[]>((App)Application.Current, "ApplyFilter", values);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task LoadFilterItems(string cat_Id)
        {
            IsBusy = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format(Constants.BaseUrl + "filter/get/" + cat_Id + "", string.Empty));
                    var result = await client.GetStringAsync(uri);
                    var FilterList = JsonConvert.DeserializeObject<List<FilterModel>>(result);
                    FilterData = new ObservableCollection<FilterModel>(FilterList);
                    foreach (var price in FilterData)
                    {
                        MaxPrice = Convert.ToInt64(price.maxprice);
                    }
                }
                UpperPrice = MaxPrice;
            }
            IsBusy = false;
        }
    }
}
