using RS_SHOP_Dev.Models.OrderModels;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.Payment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentMainPage : ContentPage
    {
        List<OrderResponse> orderResponses;
        Models.PaymentModel.PaymentListModel paymentModel;
        string Categ_Id;
        public PaymentMainPage(List<OrderResponse> resultContent, string cat_id, Models.PaymentModel.PaymentListModel selectedCard)
        {
            InitializeComponent();
            this.BindingContext = new PaymentViewModel();
            orderResponses = resultContent;
            Categ_Id = cat_id;
            paymentModel = selectedCard;
        }

        protected override void OnAppearing()
        {
            if (!(paymentModel == null))
                (this.BindingContext as PaymentViewModel).LoadScreen(paymentModel);
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Confirm!", "You will navigate to home screen", "Yes Take Me To Home", "No");
                if (result) App.Current.MainPage = new NavigationPage(new Home(2)); ;
            });

            return true;
        }

        private async void btnpay_Clicked(object sender, EventArgs e)
        {
            await (this.BindingContext as PaymentViewModel).PaymentRequest(orderResponses, Categ_Id);
        }
    }
}