using APIRepository.Models.Custom;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.CartPages;
using RS_SHOP_Dev.Views.Merchandise;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
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
    public partial class ProductDetailPage : ContentPage
    {
        string UserId, ProdId, Categ_Id;
        Products product;
        public ProductDetailPage(Products products, string cat_Id)
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            ProdId = products.product.PRODUCT_ID.ToString();
            Categ_Id = cat_Id;
            prodName.Text = products.product.PRODUCT_NAME;
            prodDesc.Text = products.product.PRODUCT_DESCRIPTION;
            Price.Text = products.product.PRODUCT_PRICE.ToString();
            product = products;
            this.BindingContext = new ProductsViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Quantity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                int Quantity = Convert.ToInt32(sender.GetType().GetProperty(e.PropertyName).GetValue(sender));
                Price.Text = Convert.ToDecimal(product.product.PRODUCT_PRICE * Quantity).ToString();
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (!(UserId.Equals("0")))
            {
                await (this.BindingContext as ProductsViewModel).AddToCart(UserId, ProdId, Quantity.Text, Categ_Id);
                if (Categ_Id.Equals("10"))
                    await Navigation.PushAsync(new ProductCartPage());
                else
                    await Navigation.PushAsync(new MerchCartPage());
            }else
                await PopupNavigation.Instance.PushAsync(new AuthAlert());
        }
    }
}