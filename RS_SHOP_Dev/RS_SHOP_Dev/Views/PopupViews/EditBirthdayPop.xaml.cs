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
    public partial class EditBirthdayPop : PopupPage
    {
        public EditBirthdayPop()
        {
            InitializeComponent();
            this.BindingContext = new ProfileViewModel(this);
            MainDatePicker.MaximumDate = DateTime.Now;
        }

        private void MainDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
          //  MainLabel.Text = e.NewDate.ToLongDateString();

            //  e.NewDate.ToLongDateString();

        }

        private void Handle_Birthday_Cancel(object sender, EventArgs e)
        {
            //Popup Close
            PopupNavigation.Instance.PopAsync();
            //  Navigation.PushAsync(new ProfileEdit());
        }
    }
}