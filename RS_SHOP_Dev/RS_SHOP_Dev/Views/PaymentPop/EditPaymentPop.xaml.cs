using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Models.PaymentModel;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.PaymentPop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPaymentPop : PopupPage
    {
        string UserId;
        string cardExpireDate, cardHolderName, cardNumber, cardType;
        PaymentListModel paymentListModel;
        public EditPaymentPop(PaymentListModel selectedItem)
        {
            InitializeComponent();
            UserId = Application.Current.Properties["USER_ID"].ToString();
            paymentListModel = selectedItem;
            this.BindingContext = new PaymentViewModel();
            pickerMonth.ItemsSource = new List<string>
            {
                "01","02","03","04","05","06","06","07","08","09","10","11","12"
            };
            pickerYear.ItemsSource = new List<string>
            {
                "2019","2020","2021","2022","2023","2024","2025","2026","2027","2028","2029","2030"
            };
        }
        protected override void OnAppearing()
        {
            (this.BindingContext as PaymentViewModel).LoadCardsOnPopup(paymentListModel);
        }

        private void Handle_Payment_Cancel(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}