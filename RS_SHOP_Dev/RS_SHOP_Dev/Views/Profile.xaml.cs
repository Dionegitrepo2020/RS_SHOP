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
    public partial class Profile : ContentPage
    {
        string UserId;
        public Profile()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            this.BindingContext = new ProfileViewModel(UserId);
 
        }

        protected async override void OnAppearing()
        {
            (this.BindingContext as ProfileViewModel).LoadUserData(UserId);
        }

        private void Profile_Edit_Handeled(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileEdit());
        }

        private void Click_Orders(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Promotion());
        }

        private void Handle_Notification(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Notification());
        }

        private void Handle_ShippingAddress(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShippingAddress());
        }

        private void Handle_Payment(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentMethod());
        }
    }
}