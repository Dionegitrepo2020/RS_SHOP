using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using RS_SHOP_Dev.Views.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Orders : ContentPage
    {
        string UserId;
        public Orders()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            this.BindingContext = new OrderViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (this.BindingContext as OrderViewModel).LoadOrderProducts(UserId);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (RS_SHOP_Dev.Models.OrderModels.OrderModel)((Button)sender).CommandParameter;
            string OrderId = selectedItem.orderm.ORDER_ID.ToString();
            string OrderRefNo = selectedItem.orderm.ORDER_REF_NO.ToString();
            string Categ_Id;
            if (OrderRefNo.StartsWith("F"))
                Categ_Id = "10";
            else
                Categ_Id = "11";
            Navigation.PushAsync(new WalletOrderPage(OrderId, Categ_Id));
        }
    }
}