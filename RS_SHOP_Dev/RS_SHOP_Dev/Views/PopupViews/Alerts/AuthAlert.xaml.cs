using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class AuthAlert : PopupPage
    {
        public AuthAlert()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
            PopupNavigation.Instance.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true; // Disable back button
        }
    }
}