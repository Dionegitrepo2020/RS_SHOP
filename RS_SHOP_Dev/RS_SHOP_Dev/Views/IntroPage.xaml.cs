using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.CustomRenderers;
using RS_SHOP_Dev.Resources;
using RS_SHOP_Dev.Views.AddressPop;
using RS_SHOP_Dev.Views.PaymentPop;
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
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            // show the loading page...
              Navigation.PushAsync(new LoginPage());
            
          //  PopupNavigation.Instance.PushAsync(new EditPaymentPop());
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                SnackB.Message = AppResources.IntroSnackNoInternet;
                SnackB.BackgroundColor = Color.Red;
                SnackB.IsOpen = !SnackB.IsOpen;
            }
            if (CrossConnectivity.Current.IsConnected)
            {
                SnackB.IsOpen = false;
                SnackB.Message = AppResources.IntroSnackInternet;
                SnackB.BackgroundColor = Color.Green;
                SnackB.IsOpen = true;
                await Task.Delay(5000);
                SnackB.IsOpen = false;
            }
            //await DisplayAlert("Connexion Status", "Connexion Status = " + e.IsConnected, "OK");

        }
    }
}