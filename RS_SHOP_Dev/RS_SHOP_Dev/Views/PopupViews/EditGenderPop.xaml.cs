using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.PopupViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditGenderPop : PopupPage
    {

        string UserId;
        public EditGenderPop()
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            this.BindingContext = new ProfileViewModel(UserId);

        }

       

        private void Handle_Gender_Cancel(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string item = radioGenderGroup.SelectedItem.ToString();
            await (this.BindingContext as ProfileViewModel).UpdateGender(item,UserId);
            MessagingCenter.Send<App>((App)Application.Current, "OnCategoryCreated");
            await PopupNavigation.Instance.PopAsync();
        }
    }
}