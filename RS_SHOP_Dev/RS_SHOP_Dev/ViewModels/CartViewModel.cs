using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.CartModels;
using RS_SHOP_Dev.Models.OrderModels;
using RS_SHOP_Dev.Models.PaymentModel;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views;
using RS_SHOP_Dev.Views.Payment;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using RS_SHOP_Dev.Views.Wallets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class CartViewModel : AcivityIndicatorHelper
    {
        private readonly CartApiService _apiServices = new CartApiService();
        private readonly OrderApiService _orderApiServices = new OrderApiService();
        private readonly PaymentApiService _apiServicesPayment = new PaymentApiService();
        ObservableCollection<CartProducts> _productsList;
        public ObservableCollection<CartProducts> CartProductsList
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

        decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }
        string _selectedStore;
        public string SelectedStore
        {
            get { return _selectedStore; }
            set
            {
                _selectedStore = value;
                OnPropertyChanged("SelectedStore");
            }
        }

        List<PaymentListModel> _paymentList;
        public List<PaymentListModel> PaymentList
        {
            get
            {
                return _paymentList;
            }
            set
            {
                _paymentList = value;
                OnPropertyChanged();
            }
        }
        PaymentListModel _selectedCard;
        public PaymentListModel SelectedCard
        {
            get
            {
                return _selectedCard;
            }
            set
            {
                _selectedCard = value;
                OnPropertyChanged("SelectedCard");
            }
        }

        public ICommand GenerateOrderCommand { get; }

        public CartViewModel()
        {
            GenerateOrderCommand = new Command(async (x) => await OrderGenerateAsync(x.ToString()));
            LoadCards((Application.Current.Properties["USER_ID"]??"0").ToString());
        }

        private async void LoadCards(string userId)
        {
            PaymentList = await _apiServicesPayment.LoadCardAsync(userId);
        }

        public async Task LoadProducts(string userId, string Cat_Id)
        {
            if (Cat_Id.Equals("10"))
            {
                var CartProduct = await _apiServices.GetFromcartService(userId);
                CartProductsList = new ObservableCollection<CartProducts>(CartProduct);
            }
            else
            {
                var CartProduct = await _apiServices.GetMerchFromcartService(userId);
                CartProductsList = new ObservableCollection<CartProducts>(CartProduct);
            }
        }

        public async Task LoadTotal(string userId, string Cat_Id)
        {
            var CartProduct = new List<CartProducts>();
            if (Cat_Id.Equals("10"))
            {
                CartProduct = await _apiServices.GetFromcartService(userId);
            }
            else
            {
                CartProduct = await _apiServices.GetMerchFromcartService(userId);
            }
            Total = 0;
            foreach (var price in CartProduct)
            {
                Total += price.cart.TB_ECOMM_PRODUCT.PRODUCT_PRICE * price.cart.CART_ITEM_QUANTITY;
            }
        }

        public async Task UpdateCart(string cart_Id, string cart_Qty)
        {
            await _apiServices.UpdateCartService(cart_Id, cart_Qty);
        }

        public async Task GenerateOrderAsync(string userId, string TotalAmt, string selectedStore)
        {
            string accessToken;
            IsBusy = true;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "oder/add", string.Empty));
                JObject jObject = new JObject();
                jObject.Add("USER_ID", userId);
                jObject.Add("ORDER_AMOUNT", TotalAmt);
                jObject.Add("PICKUP_STORE_ID", selectedStore);
                jObject.Add("PRODUCT_ID", CartProductsList.Select(a => a.cart.PRODUCT_ID).FirstOrDefault());
                jObject.Add("PRODUCT_QUANTITY", CartProductsList.Select(a => a.cart.CART_ITEM_QUANTITY).FirstOrDefault());
                var json = JsonConvert.SerializeObject(jObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                var contents = await response.Content.ReadAsStringAsync();
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
                accessToken = jwtDynamic.Value<string>("ORDER_QR_TAG");
                //await Application.Current.MainPage.Navigation.PushAsync(new OrderSuccessQR(accessToken, TotalAmt));
            }
            IsBusy = false;

        }

        public async Task DeleteCartAsync(int cart_ID)
        {
            await _apiServices.DeleteCartService(cart_ID);
        }

        public async Task ClearCart(string userId)
        {
            await _apiServices.DeleteAllCartService(userId);
        }

        //Order Generation Method for Both FD and M
        public async Task OrderGenerateAsync(string cat_id)
        {
            string UserId = Application.Current.Properties["USER_ID"].ToString();
            await LoadProducts(UserId, cat_id);
            await LoadTotal(UserId, cat_id);

            var ProdList = new JArray() as dynamic;
            foreach (var item in CartProductsList)
            {
                JObject jObject1 = new JObject();
                jObject1.Add("PRODUCT_ID", item.cart.PRODUCT_ID);
                jObject1.Add("PRODUCT_QUANTITY", item.cart.CART_ITEM_QUANTITY);
                jObject1.Add("PRODUCT_AMOUNT", item.cart.CART_ITEM_QUANTITY * item.cart.TB_ECOMM_PRODUCT.PRODUCT_PRICE);
                ProdList.Add(jObject1);
            }
            JObject jObject = new JObject();
            jObject.Add("USER_ID", UserId);
            jObject.Add("CATEGORY_ID", cat_id);
            jObject.Add("ORDER_AMOUNT", Total);
            jObject.Add("PICKUP_STORE_ID", SelectedStore ?? "10");
            jObject.Add("order_item", ProdList);
            var ResultContent = await _orderApiServices.GenerateFoodDrinkOrderService(jObject);
            var QRTag = ResultContent.Select(s => s.ORDER_QR_TAG).FirstOrDefault();
            var Order_ID = ResultContent.Select(s => s.ORDER_ID).FirstOrDefault().ToString();
            var Amount = ResultContent.Select(s => s.ORDER_AMOUNT).FirstOrDefault();
            if (!string.IsNullOrEmpty(QRTag) && cat_id == "10")
            {
                App.Current.MainPage = new NavigationPage(new Home(1));
                await Application.Current.MainPage.Navigation.PushAsync(new WalletOrderPage(Order_ID, cat_id));
                await ClearCart(UserId);
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PaymentMainPage(ResultContent,cat_id,SelectedCard));
            }
        }
    }
}
