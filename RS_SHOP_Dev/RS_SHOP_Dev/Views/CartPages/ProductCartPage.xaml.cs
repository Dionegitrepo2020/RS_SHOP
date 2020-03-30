
using RS_SHOP_Dev.Controls;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.CartModels;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.FoodDrinks;
using RS_SHOP_Dev.Views.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.CartPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCartPage : ContentPage
    {
        
        string UserId;
        public ProductCartPage()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            this.BindingContext = new CartViewModel();
        }
        protected async override void OnAppearing()
        {
            await (this.BindingContext as CartViewModel).LoadTotal(UserId,"10");
            await (this.BindingContext as CartViewModel).LoadProducts(UserId,"10");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new FoodDrinkCheckout(TotalPrice.Text, UserId));
        }

        private async void Quantity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                CustomStepper customStepper = (CustomStepper)sender;
                StackLayout listViewItem = (StackLayout)customStepper.Parent;
                Label label = (Label)listViewItem.Children[0];
                Label pPrice = (Label)listViewItem.Children[1];
                Label cPrice = (Label)listViewItem.Children[2];
                string Cart_Id = label.Text;
                string prodPrice = pPrice.Text;
                string cartPrice = cPrice.Text;
                string Cart_Qty = sender.GetType().GetProperty(e.PropertyName).GetValue(sender).ToString();
                await (this.BindingContext as CartViewModel).UpdateCart(Cart_Id, Cart_Qty);
                await(this.BindingContext as CartViewModel).LoadTotal(UserId,"10");
                cPrice.Text = (Convert.ToDecimal(Cart_Qty) * Convert.ToDecimal(prodPrice)).ToString();
            }
            //DoRefresh();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var selectedItem = (CartProducts)((Button)sender).CommandParameter;
            var Cart_ID = selectedItem.cart.CART_ITEM_ID;
            await (this.BindingContext as CartViewModel).DeleteCartAsync(Cart_ID);
            await (this.BindingContext as CartViewModel).LoadTotal(UserId,"10");
            await (this.BindingContext as CartViewModel).LoadProducts(UserId,"10");
            if (TotalPrice.Text == "0")
                await Navigation.PopAsync();
        }
    }
}