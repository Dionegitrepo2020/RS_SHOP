using Plugin.InputKit.Shared.Controls;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.FoodDrinks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDrinkCheckout : ContentPage
    {
        decimal TotalAmt;
        string UserId;
        public FoodDrinkCheckout(string totalPrice, string userId)
        {
            InitializeComponent();
            this.BindingContext = new CartViewModel();
            TotalAmt = Convert.ToDecimal(totalPrice);
            TotalPrice.Text = TotalAmt.ToString();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as CartViewModel).LoadProducts(UserId,"10");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string selectedStore = radioGroup.SelectedItem.ToString();
            await (this.BindingContext as CartViewModel).GenerateOrderAsync(UserId, TotalPrice.Text, selectedStore);
            await (this.BindingContext as CartViewModel).ClearCart(UserId);
        }
    }
}