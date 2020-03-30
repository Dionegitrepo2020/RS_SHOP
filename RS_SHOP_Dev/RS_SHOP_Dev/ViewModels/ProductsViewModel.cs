using APIRepository.Models.Custom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.SearchFilterModel;
using RS_SHOP_Dev.Models.ShoppingModel;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views.CartPages;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
//using RS_SHOP_Dev.Models.ShoppingModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Products = APIRepository.Models.Custom.Products;

namespace RS_SHOP_Dev.ViewModels
{
    public class ProductsViewModel : AcivityIndicatorHelper
    {
        private readonly CartApiService _apiServices = new CartApiService();

        ObservableCollection<Products> _productsList;
        public ObservableCollection<Products> ProductsList
        {
            get
            {
                return _productsList;
            }
            set
            {
                _productsList = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<SearchItems> _searchsList;
        public ObservableCollection<SearchItems> SearchList
        {
            get
            {
                return _searchsList;
            }
            set
            {
                _searchsList = value;
                OnPropertyChanged();
            }
        }

        public string AdUnitId { get; set; } = "ca-app-pub-3940256099942544/6300978111";

        public ProductsViewModel()
        {
            if (Device.RuntimePlatform == Device.iOS)
                AdUnitId = "ca-app-pub-3940256099942544/6300978111";
            else if (Device.RuntimePlatform == Device.Android)
                AdUnitId = "ca-app-pub-2380813622558954/7114318827";
        }

        public async Task LoadItems(string categ, string subcateg, string priceFrom, string priceTo)
        {
            IsBusy = true;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "item/listallitem/" + categ + "/" + subcateg + "/" + priceFrom + "/" + priceTo + "", string.Empty));
                var result = await client.GetStringAsync(uri);
                var ProdList = JsonConvert.DeserializeObject<List<Products>>(result);
                ProductsList = new ObservableCollection<Products>(ProdList);
            }
            IsBusy = false;
        }

        public async Task AddToCart(string userId, string prodId, int quanity,string Cat_Id)
        {
            TB_ECOMM_CART_ITEM tB_ECOMM_CART_ITEM = new TB_ECOMM_CART_ITEM();
            tB_ECOMM_CART_ITEM.USER_ID = Convert.ToInt64(userId);
            tB_ECOMM_CART_ITEM.PRODUCT_ID = Convert.ToInt64(prodId);
            tB_ECOMM_CART_ITEM.CART_ITEM_QUANTITY = quanity;
            tB_ECOMM_CART_ITEM.CATEGORY_ID = Convert.ToInt64(Cat_Id);

            var contents = await _apiServices.AddtocartService(tB_ECOMM_CART_ITEM);
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            var accessToken = jwtDynamic.Value<string>("Status");
            var Message = jwtDynamic.Value<string>("Message");
            if (!(accessToken == "Success"))
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(Message));
            }
                //await Application.Current.MainPage.Navigation.PushAsync(new ProductCartPage());
                //await Application.Current.MainPage.DisplayAlert("", Message, "Ok");
        }

        public async void SearchItems(string term, string categ_Id)
        {
            IsBusy = true;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "item/searchsuggest/" + term + "/"+categ_Id, string.Empty));
                var result = await client.GetStringAsync(uri);
                var ProdList = JsonConvert.DeserializeObject<List<SearchItems>>(result);
                SearchList = new ObservableCollection<SearchItems>(ProdList);
            }
            IsBusy = false;
        }

        public async Task FindItemByIdAsync(int pRODUCT_ID)
        {
            IsBusy = true;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "item/listallitem/" + pRODUCT_ID + "", string.Empty));
                var result = await client.GetStringAsync(uri);
                var ProdList = JsonConvert.DeserializeObject<List<Products>>(result);
                ProductsList = new ObservableCollection<Products>(ProdList);
            }
            IsBusy = false;
        }
    }
}
