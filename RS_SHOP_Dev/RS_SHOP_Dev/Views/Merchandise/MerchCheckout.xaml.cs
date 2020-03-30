
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.Payment;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.Merchandise
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MerchCheckout : ContentPage
    {
        string UserId;
        public MerchCheckout(string TotalPrice, string userId)
        {
            InitializeComponent();
            Total.Text = TotalPrice;
            UserId = userId;
            this.BindingContext = new CartViewModel();
        }

        private async void ConfirmOrderButton_Clicked(object sender, EventArgs e)
        {
            await (this.BindingContext as CartViewModel).OrderGenerateAsync("11");
            await (this.BindingContext as CartViewModel).ClearCart(UserId);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentMain());
        }
    }
}