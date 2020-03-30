using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.Wallets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletOrderPage : ContentPage
    {
        string Order_Id,Categ_Id;
        public WalletOrderPage(string order_ID, string cat_id)
        {
            InitializeComponent();
            Order_Id = order_ID;
            Categ_Id = cat_id;
            this.BindingContext = new OrderViewModel();
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as OrderViewModel).LoadOrderByOrderId(Order_Id, Categ_Id);
        }
    }
}