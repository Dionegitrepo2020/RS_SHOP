using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Models.ShipAddressModel;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.AddressPop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAddressPop : PopupPage
    {
        string UserId;
        ShipAddressModel shipAddressModel;
        public EditAddressPop(ShipAddressModel selectedItem)
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            shipAddressModel = selectedItem;
            this.BindingContext = new AddressViewModel();
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as AddressViewModel).LoadAddressOnPopup(shipAddressModel);
        }


        private void Address_Cancel(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}