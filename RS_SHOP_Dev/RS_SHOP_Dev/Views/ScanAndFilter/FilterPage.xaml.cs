using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.ScanAndFilter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        string Cat_Id;
        public FilterPage(string cat_Id)
        {
            InitializeComponent();
            this.BindingContext = new FilterViewModel();
            Cat_Id = cat_Id;
        }
        protected async override void OnAppearing()
        {
            await (this.BindingContext as FilterViewModel).LoadFilterItems(Cat_Id);
        }

        private void closeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}