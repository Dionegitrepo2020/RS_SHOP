using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Models;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Forgot : ContentPage
    {
        string Activation_Code;
        public Forgot(string activationCode)
        {
            InitializeComponent();
            Activation_Code = activationCode;
            this.BindingContext = new UsersViewModel(this);
        }
        private async void OnUserAnimationPupup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new ResetPasswordPopupView());
        }

        private async void ChangePassword_Button_Clicked(object sender, EventArgs e)
        {
            PasswordResetModel passwordResetModel = new PasswordResetModel();
            passwordResetModel.Password = txt_Password.Text;
            passwordResetModel.ConfirmPassword = txt_ConfirmPassword.Text;
            passwordResetModel.ActivationCode = Activation_Code;
            await (this.BindingContext as UsersViewModel).ResetPasswordAsync(passwordResetModel);
        }
    }
}