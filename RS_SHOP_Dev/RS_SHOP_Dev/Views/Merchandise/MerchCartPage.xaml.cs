using RS_SHOP_Dev.Controls;
using RS_SHOP_Dev.Models.CartModels;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.FoodDrinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.Merchandise
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MerchCartPage : ContentPage
    {
        string UserId;
        public MerchCartPage()
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            this.BindingContext = new CartViewModel();
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as CartViewModel).LoadTotal(UserId,"11");
            await (this.BindingContext as CartViewModel).LoadProducts(UserId,"11");
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
                await (this.BindingContext as CartViewModel).LoadTotal(UserId,"11");
                cPrice.Text = (Convert.ToDecimal(Cart_Qty) * Convert.ToDecimal(prodPrice)).ToString();
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var selectedItem = (CartProducts)((Button)sender).CommandParameter;
            var Cart_ID = selectedItem.cart.CART_ITEM_ID;
            await (this.BindingContext as CartViewModel).DeleteCartAsync(Cart_ID);
            await (this.BindingContext as CartViewModel).LoadTotal(UserId,"11");
            await (this.BindingContext as CartViewModel).LoadProducts(UserId,"11");
            if (TotalPrice.Text == "0")
                await Navigation.PopAsync();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MerchCheckout(TotalPrice.Text, UserId));
        }
    }
}