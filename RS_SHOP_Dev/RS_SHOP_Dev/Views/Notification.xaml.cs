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
    public partial class Notification : ContentPage
    {
        string UserId;
        public Notification()
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            //this.BindingContext = new CartViewModel();
        }
        protected async override void OnAppearing()
        {
            //(this.BindingContext as CartViewModel).LoadProducts(UserId);
        }
    }
}