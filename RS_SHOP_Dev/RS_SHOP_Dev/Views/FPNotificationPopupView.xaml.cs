using Rg.Plugins.Popup.Pages;
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
    public partial class FPNotificationPopupView : PopupPage
    {
        public FPNotificationPopupView()
        {
            InitializeComponent();
        }

        void Handle_FPNotification_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
            //  PopupNavigation.Instance.PopAsync(true);
            //  await PopupNavigation.Instance.PushAsync(new Forgot());
        }
    }
}