using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Models.PaymentModel;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.PaymentPop;
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
    public partial class PaymentMethod : ContentPage
    {
        string UserId;
        public PaymentMethod()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            this.BindingContext = new PaymentViewModel();
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as PaymentViewModel).LoadCards(UserId);
        }

        private async void PaymentEdit(object sender, EventArgs e)
        {
            var selectedItem = (PaymentListModel)((ImageButton)sender).CommandParameter;
            await PopupNavigation.Instance.PushAsync(new EditPaymentPop(selectedItem));
        }

        private async void DeletePayment(object sender, EventArgs e)
        {
            var selectedItem = (PaymentListModel)((ImageButton)sender).CommandParameter;
            await (this.BindingContext as PaymentViewModel).RemoveCardData(selectedItem.CARD_ID);
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditPaymentPop(new PaymentListModel()));

          
        }
    }

}