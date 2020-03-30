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
    public partial class ParentDetailsAlert : PopupPage
    {
        public ParentDetailsAlert(string alert)
        {
            InitializeComponent();
            Message.Text = alert;
            this.BindingContext = new UsersViewModel(this);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}