using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using RS_SHOP_Dev.Views.TabPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class UsersViewModel : AcivityIndicatorHelper
    {
        private readonly LoginSignupService _apiServices = new LoginSignupService();
        public SignupModel SignupModel { get; set; } = new SignupModel();
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public ICommand LoginInCommand { get; }
        public ICommand SignUpCommand { get; }

        //Reset password
        public ForgotPasswordModel ForgotPasswordModel { get; set; } = new ForgotPasswordModel();
        public ICommand ForgotPasswordCommand { get; }

        public ICommand OnLoginWithFacebookCommand { get; set; }

        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        IFacebookClient _facebookService = CrossFacebookClient.Current;
        private Page _page;
        //
        public UserProfile User { get; set; } = new UserProfile();
        string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        public string Name
        {
            get => User.Name;
            set => User.Name = value;
        }

        public string Email
        {
            get => User.Email;
            set => User.Email = value;
        }

        public Uri Picture
        {
            get => User.Picture;
            set => User.Picture = value;
        }

        public bool IsLoggedIn { get; set; }

        public string Token { get; set; }

        //SignUpModel
        public string FullName
        {
            get => SignupModel.FullName;
            set => SignupModel.FullName = value;
        }

        public string UserEmail
        {
            get => SignupModel.UserEmail;
            set => SignupModel.UserEmail = value;
        }

        public string Password
        {
            get => SignupModel.Password;
            set => SignupModel.Password = value;
        }

        public string ConfirmPassword
        {
            get => SignupModel.ConfirmPassword;
            set => SignupModel.ConfirmPassword = value;
        }

       

        public string ParentName
        {
            get => SignupModel.ParentName;
            set => SignupModel.ParentName = value;
        }

        public string ParentID
        {
            get => SignupModel.ParentID;
            set => SignupModel.ParentID = value;
        }



        //Condition
        private string _ConditionId = "0";
       // private string _ConditionId { get; set; }
        public string ConditionId
        {

            get { return _ConditionId; }

            set
            {
                _ConditionId = value;
                OnPropertyChanged();
            }
        }

        private  DateTime _dateOfBirth { get; set; }
        public  DateTime DateOfBirth
        {

            get { return _dateOfBirth; }

            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }


        public ICommand OnLoginWithGoogleCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        private readonly IGoogleClientManager _googleClientManager;
        public event PropertyChangedEventHandler PropertyChanged;
        //
        public UsersViewModel(Page page)
        {
            _page = page;
          
            LoginInCommand = new Command(async () => await LoginAsync());
          //  SignUpCommand = new Command(async () => await SignupAsync());
            OnLoginWithFacebookCommand = new Command(async () => await LoginFacebookAsync());
            OnLoginWithGoogleCommand = new Command<AuthNetwork>(async (data) => await LoginAsync(data));
            _googleClientManager = CrossGoogleClient.Current;
            IsLoggedIn = false;

            //Reset Password
            ForgotPasswordCommand = new Command(async () => await ForgotSync());
        }

       

        private async Task LoginAsync()
        {
            if (!ValidationHelper.IsFormValid(LoginModel, _page)) { return; }
            await LoginApiAsync();

        }

        private async Task LoginApiAsync()
        {
            IsBusy = true;
            var contents = await _apiServices.LoginAsync(LoginModel.Email, LoginModel.Password);
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
            Message = jwtDynamic.Value<string>("Message");
            var UserId= jwtDynamic.Value<string>("User_Id");
            if (!(Message == "Login Successfully"))
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(Message));
            }
            else
            {
                
                App.IsUserLoggedIn = true;
                Application.Current.Properties["USER_ID"] = UserId;
                await Application.Current.MainPage.Navigation.PushAsync(new Home(0));
            }
            IsBusy = false;
        }

        
        public async Task SignupAsync()
        {
            if (!ValidationHelper.IsFormValid(SignupModel, _page)) { return; }

            await SignupApiAsync(ConditionId);
           
        }


        //Signup Change
        private async Task SignupApiAsync(string ConditionId)
        {
           
            IsBusy = true;

            var SignupStatus = await _apiServices.RegistrationAsync1(SignupModel.FullName,
                        SignupModel.Password, SignupModel.UserEmail, SignupModel.DOB.ToString(), ConditionId, SignupModel.ParentName, SignupModel.ParentID);
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(SignupStatus);
            var accessToken = jwtDynamic.Value<string>("Message");
            if (!(accessToken == "User SuccessFully Saved."))
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(accessToken));
                //   await Application.Current.MainPage.DisplayAlert("", accessToken, "Ok");
            }
            else
            {

                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            IsBusy = false;
        }


        async Task LoginFacebookAsync()
        {
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null) return;

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            var socialLoginData = new NetworkAuthData
                            {
                                Email = facebookProfile.Email,
                                Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                                Id = facebookProfile.Id
                            };
                            SignupModel.FullName = $"{facebookProfile.FirstName} {facebookProfile.LastName}";
                            SignupModel.UserEmail = facebookProfile.Email;
                            var contents = await _apiServices.RegistrationAsync(SignupModel.FullName,"", SignupModel.UserEmail);
                            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
                            var UserId = jwtDynamic.Value<string>("USER_ID");
                            App.IsUserLoggedIn = true;
                            Application.Current.Properties["USER_ID"] = UserId;
                            await App.Current.MainPage.Navigation.PushAsync(new Home(0));
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task LoginAsync(AuthNetwork authNetwork)
        {
            await LoginGoogleAsync(authNetwork);
        }
        async Task LoginGoogleAsync(AuthNetwork authNetwork)
        {
            _googleClientManager.OnLogin += OnLoginCompleted;
            try
            {
                await _googleClientManager.LoginAsync();
            }
            catch (GoogleClientSignInNetworkErrorException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
            catch (GoogleClientSignInCanceledErrorException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
            catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
            catch (GoogleClientSignInInternalErrorException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
            catch (GoogleClientBaseException e)
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(e.Message));
            }
        }

        private async void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;
                User.Name = googleUser.Name;
                User.Email = googleUser.Email;
                User.Picture = googleUser.Picture;
                var GivenName = googleUser.GivenName;
                var FamilyName = googleUser.FamilyName;

                SignupModel.FullName = $"{googleUser.Name}";
                SignupModel.UserEmail = googleUser.Email;
                SignupModel.Password = "";
                var contents = await _apiServices.RegistrationAsync(SignupModel.FullName,"", SignupModel.UserEmail);
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
                var UserId = jwtDynamic.Value<string>("USER_ID");
                //IsLoggedIn = true;
                Application.Current.Properties["USER_ID"] = UserId;
                App.IsUserLoggedIn = true;
                //await App.Current.MainPage.Navigation.PushAsync(new Home());
                App.Current.MainPage = new NavigationPage(new Home(0));
                //Application.Current.MainPage = new AppHomeShell();
                //await Shell.Current.GoToAsync("//main");
                var token = CrossGoogleClient.Current.ActiveToken;
                Token = token;
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(loginEventArgs.Message));
            }

            _googleClientManager.OnLogin -= OnLoginCompleted;

        }

        public void Logout()
        {
            _googleClientManager.OnLogout += OnLogoutCompleted;
            _googleClientManager.Logout();
        }

        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            User.Email = "Offline";
            _googleClientManager.OnLogout -= OnLogoutCompleted;
        }

        //Reset Password
        private async Task ForgotSync()
        {
            if (!ValidationHelper.IsFormValid(ForgotPasswordModel, _page)) { return; }
            await ForgotApiAsync();

        }

        private async Task ForgotApiAsync()
        {
            
            IsBusy = true;
            var SendMail = await _apiServices.ForgotAsync(ForgotPasswordModel);
            if (SendMail.Contains("Reset password link has been sent to your email id."))
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(SendMail));
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert("Something went wrong."));
            }
            IsBusy = false;
        }

        public async Task ResetPasswordAsync(PasswordResetModel passwordResetModel)
        {
            IsBusy = true;
            var Message = await _apiServices.PasswordResetAsync(passwordResetModel);
            if (Message.Contains("New password updated successfully"))
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert(Message));
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new LoginAlert("Something went wrong."));
            }
            IsBusy = false;
        }
    }
}
