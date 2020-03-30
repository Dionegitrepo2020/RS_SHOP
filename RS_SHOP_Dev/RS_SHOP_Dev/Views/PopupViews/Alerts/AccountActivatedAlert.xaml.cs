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
    public partial class AccountActivatedAlert : PopupPage
    {
        string UserId;
        public AccountActivatedAlert(string message, string userId)
        {
            InitializeComponent();
            Message.Text = message;
            UserId = userId;
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = true;
            Application.Current.Properties["USER_ID"] = UserId;
            await PopupNavigation.Instance.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new Home(0));
        }
    }
}