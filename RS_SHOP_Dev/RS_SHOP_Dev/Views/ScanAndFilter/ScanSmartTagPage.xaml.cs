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
    public partial class ScanSmartTagPage : ZXingScannerPage
    {
        public ScanSmartTagPage()
        {
            InitializeComponent();
        }

        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

              //  await (this.BindingContext as ProductsViewModel).AddToCart(UserId, result.Text, 1, Cat_Id);
              //  await Navigation.PushAsync(new ProductCartPage());
            });
        }
    }
}