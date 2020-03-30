using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Models.ShipAddressModel;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.AddressPop;
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
    public partial class ShippingAddress : ContentPage
    {
        string UserId;
        public ShippingAddress()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            this.BindingContext = new AddressViewModel();
            MessagingCenter.Subscribe<App>((App)Application.Current, "OnCategoryCreated", (sender) =>
            {
                OnAppearing();
            });
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as AddressViewModel).LoadAddressList(UserId);
        }


        private async void AddressEdit(object sender, EventArgs e)
        {
            var selectedItem = (ShipAddressModel)((ImageButton)sender).CommandParameter;
            await PopupNavigation.Instance.PushAsync(new EditAddressPop(selectedItem));

        }

        private async void DeleteAddress(object sender, EventArgs e)
        {
            var selectedItem = (ShipAddressModel)((ImageButton)sender).CommandParameter;
            await (this.BindingContext as AddressViewModel).RemoveAddressData(selectedItem.ADDRESS_ID);
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditAddressPop(new ShipAddressModel()));
        }
    }
}