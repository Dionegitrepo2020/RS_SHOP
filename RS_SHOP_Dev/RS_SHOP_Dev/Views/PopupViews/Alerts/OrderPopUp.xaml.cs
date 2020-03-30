using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Views.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.PopupViews.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPopUp : PopupPage
    {
        string OrderId,Categ_Id;
        public OrderPopUp(string orderId, string cat_id)
        {
            InitializeComponent();
            OrderId = orderId;
            Categ_Id = cat_id;
        }

        private async void BtnCheckStatus_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            App.Current.MainPage = new NavigationPage(new Home(1));
            await Navigation.PushAsync(new WalletOrderPage(OrderId, Categ_Id));
        }

        private void BtnKeepShopping_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            Navigation.PopToRootAsync();
            App.Current.MainPage = new NavigationPage(new Home(2));
        }
    }
}