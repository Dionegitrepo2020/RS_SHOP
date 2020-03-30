using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Resources;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static RS_SHOP_Dev.Models.DropdownModel.CountyState;

namespace RS_SHOP_Dev.Views.PopupViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCountryPop : PopupPage
    {
        string CountryID;
        string UserId;
        string CountryAlert = AppResources.EditCountryPop_CountryAlert;
        string CityAlert = AppResources.EditCountryPop_CityAlert;
        public EditCountryPop()
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();

            this.BindingContext = new CountryStateViewModel();
 
        }

        protected async override void OnAppearing()
        {
            
            (this.BindingContext as CountryStateViewModel).LoadCountryList();
        }

        private void Handle_Country_Cancel(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Root selectedItem = (Root)picker.SelectedItem;
            var item = selectedItem.country.COUNTRY_ID;
            (this.BindingContext as CountryStateViewModel).LoadCityList(item);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            Root Country = (Root)PkrCountry.SelectedItem;
           
            Root City = (Root)PkrCity.SelectedItem;
            
            if (PkrCountry.SelectedIndex == -1)
            {
                await PopupNavigation.Instance.PushAsync(new CountryAlert(CountryAlert));

            }
            else if(PkrCity.SelectedIndex == -1)
            {
                await PopupNavigation.Instance.PushAsync(new CountryAlert(CityAlert));
            }
            else
            {
                string CountryName = Country.country.COUNTRY_NAME;
                string CityName = City.city.CITY_NAME;

                await (this.BindingContext as CountryStateViewModel).UpdateCountryCity(CountryName, CityName, UserId);
                MessagingCenter.Send<App>((App)Application.Current, "OnCategoryCreated");
                await PopupNavigation.Instance.PopAsync();
            }
            

        }
    }
}