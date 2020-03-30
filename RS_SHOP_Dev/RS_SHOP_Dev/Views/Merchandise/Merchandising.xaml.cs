using APIRepository.Models.Custom;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.ScanAndFilter;
using RS_SHOP_Dev.Views.SearchFilterItems;
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
    public partial class Merchandising : ContentPage
    {
        string Cat_Id;
        public Merchandising()
        {
            InitializeComponent();
            this.BindingContext = new ProductsViewModel();
            Cat_Id = "11";
        }

        protected async override void OnAppearing()
        {
            FloatMenuItem1.IsVisible = false;
            FloatMenuItem2.IsVisible = false;
            FloatMenuItem3.IsVisible = false;
            await (this.BindingContext as ProductsViewModel).LoadItems("11", "0", "1", "0");
        }
        private void productList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Products products = (Products)((CollectionView)sender).SelectedItem;
            Navigation.PushAsync(new ProductDetailPage(products, Cat_Id));
        }

        private void SearchBox_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new SearchProducts(Cat_Id));
        }

        //Floating action Button

        private bool isOpen = false;
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                isOpen = true;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Show FloatMenuItem1
                FloatMenuItem1.IsVisible = true;
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);

                //Show FloatMenuItem2
                FloatMenuItem2.IsVisible = true;
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);

                //Show FloatMenuItem3
                FloatMenuItem3.IsVisible = true;
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -20, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
            }
            else
            {
                isOpen = false;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(100);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Hide FloatMenuItem1
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -20, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
                FloatMenuItem1.IsVisible = false;

                //Hide FloatMenuItem2
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -20, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);
                FloatMenuItem2.IsVisible = false;

                //Hide FloatMenuItem3
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -20, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
                FloatMenuItem3.IsVisible = false;
            }

        }

        private void FloatMenuSmartTagTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScanSmartTagPage());
        }

        private void FloatMenuQrScannerTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScanQRPage(Cat_Id));
        }

        private void FloatMenuFiltersTap(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FilterPage(Cat_Id));
        }
    }
}