using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
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
    public partial class CountryAlert : PopupPage
    {
        public CountryAlert(string message)
        {
            InitializeComponent();
            Message.Text = message;
            this.BindingContext = new ProfileViewModel(this);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}