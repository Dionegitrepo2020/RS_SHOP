using Plugin.Multilingual;
using RS_SHOP_Dev.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            picker.Items.Add("English");
            picker.Items.Add("Spanish");
            picker.SelectedItem = CrossMultilingual.Current.CurrentCultureInfo.EnglishName;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.EnglishName.Contains(picker.SelectedItem.ToString()));
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            App.Current.MainPage = new NavigationPage(new IntroPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            //Application.Current.MainPage = new IntroPage();
            App.Current.MainPage = new NavigationPage(new IntroPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string url = "https://www.realsociedad.eus/en/i/politica-de-privacidad";
            Uri uri = new Uri(url.ToString());
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void TapGestureRecognizer_Tapped_Settings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile());
        }

        private void TapGestureRecognizer_Tapped_Orders(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Orders());
        }

        private void TapGestureRecognizer_Tapped_Notification(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Notification());
        }
    }
}