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
    public partial class EditBirthdayPopup : ContentPage
    {
        public EditBirthdayPopup()
        {
            InitializeComponent();
        }

        private void MainDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
             MainLabel.Text = e.NewDate.ToLongDateString();

          //  e.NewDate.ToLongDateString();

        }
    }
}