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

namespace RS_SHOP_Dev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPopupView : PopupPage
    {
        public ResetPasswordPopupView()
        {
            InitializeComponent();
            this.BindingContext = new UsersViewModel(this);
        }

       /* void Handle_Reset_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FPNotificationPopupView());
          //  PopupNavigation.Instance.PopAsync(true);
          //  await PopupNavigation.Instance.PushAsync(new Forgot());
        }*/
    }
}