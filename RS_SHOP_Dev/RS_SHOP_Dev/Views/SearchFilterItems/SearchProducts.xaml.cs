using APIRepository.Models.Custom;
using RS_SHOP_Dev.Controls;
using RS_SHOP_Dev.Models.SearchFilterModel;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.CartPages;
using RS_SHOP_Dev.Views.Merchandise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.SearchFilterItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchProducts : ContentPage
    {
        string Categ_Id,UserId, Prod_Qty="1";
        public SearchProducts(string Cat_Id)
        {
            InitializeComponent();
            this.BindingContext = new ProductsViewModel();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            Categ_Id = Cat_Id;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SearchBox.Focus();
        }

        private void BoxedEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            productList.IsVisible = false;
            productSearchList.IsVisible = true;
            var entry = sender as Entry;
            var term = entry.Text;
            if (!string.IsNullOrEmpty(term))
            {
                (this.BindingContext as ProductsViewModel).SearchItems(term,Categ_Id);
            }
        }

        private async void productList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SearchItems item = (SearchItems)((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;
            SearchBox.Text = item.PRODUCT_NAME;
            productSearchList.IsVisible = false;
            await (this.BindingContext as ProductsViewModel).FindItemByIdAsync(item.PRODUCT_ID);
            productList.IsVisible = true;
        }

        private void Quantity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                CustomStepper customStepper = (CustomStepper)sender;
                StackLayout listViewItem = (StackLayout)customStepper.Parent;
                Label pPrice = (Label)listViewItem.Children[0];
                Label pbPrice = (Label)listViewItem.Children[2];
                string prodPrice = pPrice.Text;
                string prodBasePrice = pbPrice.Text;
                Prod_Qty = sender.GetType().GetProperty(e.PropertyName).GetValue(sender).ToString();
                pPrice.Text = (Convert.ToDecimal(Prod_Qty) * Convert.ToDecimal(prodBasePrice)).ToString();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (Products)((Button)sender).CommandParameter;
            var Prod_ID = selectedItem.product.PRODUCT_ID;
            var Product_Qty = Prod_Qty;
            var category_id = Categ_Id;
            await (this.BindingContext as ProductsViewModel).AddToCart(UserId, Prod_ID.ToString(), Convert.ToInt32(Product_Qty), category_id);
            if (Categ_Id.Equals("10"))
                await Navigation.PushAsync(new ProductCartPage());
            else
                await Navigation.PushAsync(new MerchCartPage());
        }
    }
}