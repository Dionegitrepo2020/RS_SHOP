using APIRepository.Models.Custom;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.CartPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace RS_SHOP_Dev.Views.ScanAndFilter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRPage : ZXingScannerPage
    {
        string UserId, Cat_Id;
        public ScanQRPage(string cat_Id)
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            this.BindingContext = new ProductsViewModel();
            Cat_Id = cat_Id;
        }

       
        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                await (this.BindingContext as ProductsViewModel).AddToCart(UserId, result.Text, 1, Cat_Id);
                await Navigation.PushAsync(new ProductCartPage());
            });
        }
    }
}