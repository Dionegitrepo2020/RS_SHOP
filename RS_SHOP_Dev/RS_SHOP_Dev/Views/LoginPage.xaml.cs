using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Resources;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        string ConditionId;
        string parentname_Input, parentid_Input;
        string DateMessage = AppResources.Signup_DateAlert;
        string ParentAlert = AppResources.Signup_ParentAlert;
      
        private StackLayout stacklayout;
        public LoginPage ()
		{
			InitializeComponent ();
            this.BindingContext = new UsersViewModel(this);
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            MainDatePicker.MaximumDate = DateTime.Now;
           // this.BindingContext = new ProfileViewModel(this);
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            login.IsVisible = true;
            signup.IsVisible = false;
            btnLogin.BackgroundColor = Color.FromHex("#78dfff");
            btnSignup.BackgroundColor = Color.FromHex("#e3faff");
            LoginSignupTitle.Text = AppResources.LoginTitle;
        }

        private void BtnSignup_Clicked(object sender, EventArgs e)
        {
            login.IsVisible = false;
            signup.IsVisible = true;
            btnSignup.BackgroundColor = Color.FromHex("#78dfff");
            btnLogin.BackgroundColor = Color.FromHex("#e3faff");
            LoginSignupTitle.Text = AppResources.SignupTitle;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["USER_ID"] = "0";
            App.Current.MainPage = new NavigationPage(new Home(0));
        }

        private void ForgotPass_clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ResetPasswordPopupView());
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                SnackB.Message = AppResources.IntroSnackNoInternet;
                SnackB.BackgroundColor = Color.Red;
                SnackB.IsOpen = !SnackB.IsOpen;
            }
            if (CrossConnectivity.Current.IsConnected)
            {
                SnackB.IsOpen = false;
                SnackB.Message = AppResources.IntroSnackInternet;
                SnackB.BackgroundColor = Color.Green;
                SnackB.IsOpen = true;
                await Task.Delay(5000);
                SnackB.IsOpen = false;
            }
        }

        
        private void MainDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            int currentAge = 0;
            signup.IsVisible = true;
            ParentDetails.IsVisible = false;
            

            currentAge = DateTime.Today.Year - MainDatePicker.Date.Year;
          
          //  MainLabel.Text = currentAge.ToString();

           
            if(currentAge >= 14)
            {
                ParentDetails.IsVisible = false; 

            }
            else
            {
                ParentDetails.IsVisible = true;  
            }
        }

        //Date Picker Validation And Parent Validation
        private async void BtnSignup_Clicked1(object sender, EventArgs e)
        {

            parentname_Input = ParentName.Text;
            parentid_Input = ParentID.Text;

            if (MainDatePicker.Date.Year == DateTime.Today.Year)
            {
              //  DisplayAlert("Alert", "Please select valid Date", "OK");
                  await PopupNavigation.Instance.PushAsync(new DateAlert(DateMessage));

            }
            else
            {
                if(ParentDetails.IsVisible == true)
                {
                    if (parentname_Input == null || parentid_Input == null)
                    {
                        await PopupNavigation.Instance.PushAsync(new ParentDetailsAlert(ParentAlert));
                       // DisplayAlert("Alert", "Parent Name and Parent ID Can't be Empty", "Ok");
                    }
                    else
                    {
                        await (this.BindingContext as UsersViewModel).SignupAsync();
                      //  stacklayout.Children.Clear();
                    }

                }
                else
                {
                    await (this.BindingContext as UsersViewModel).SignupAsync();
                  //  stacklayout.Children.Clear();
                }
                
            }


           
        }
    }
}