using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models;
using RS_SHOP_Dev.Models.ShoppingModel;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views;
using RS_SHOP_Dev.Views.PopupViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class ProfileViewModel:AcivityIndicatorHelper
    {
        //Prakash
        private readonly LoginSignupService _apiServices = new LoginSignupService();

        

        public ProfileModel ProfileModel { get; set; } = new ProfileModel();

        //Contact Name
        public ICommand UpdateContactInCommand { get; }

        //Date
        public ICommand UpdateDateInCommand { get; }

       

        //Gender
        public ICommand UpdateGenderInCommand { get; }


        public ICommand UpdateCountryInCommand { get; }

       

        private Page _page;

        private string userId { get; set; }


        string _item;
        public string Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        ObservableCollection<Users> _userslist;
        public ObservableCollection<Users> UsersList
        {
            get
            {
                return _userslist;
            }
            set
            {
                _userslist = value;
                OnPropertyChanged();
            }
        }


        private string _email { get; set; }
        public string Email
        {

            get { return _email; }

            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _name { get; set; }
        public string Name
        {

            get { return _name; }

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

       
        //FirstName + LastName
        private string _firstLastName;
        public string FirstLastName
        {
            get { return _firstLastName; }
            set
            {
                if (_firstLastName != value)
                {
                    _firstLastName = value;
                    OnPropertyChanged();
                    // SetPropertyChanged();
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                   
                    OnPropertyChanged("FirstName");
                 
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    
                    OnPropertyChanged("LastName");
                    
                }
            }
        }


        private string _gender { get; set; }
        public string Gender
        {

            get { return _gender; }

            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedM { get; set; }
        public bool IsCheckedM
        {

            get { return _isCheckedM; }

            set
            {
                _isCheckedM = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedF { get; set; }
        public bool IsCheckedF
        {

            get { return _isCheckedF; }

            set
            {
                _isCheckedF = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedO { get; set; }
        public bool IsCheckedO
        {

            get { return _isCheckedO; }

            set
            {
                _isCheckedO = value;
                OnPropertyChanged();
            }
        }

        private string _country { get; set; }
        public string Country
        {

            get { return _country; }

            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private string _city { get; set; }
        public string City
        {

            get { return _city; }

            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private string _image { get; set; }
        public string Image
        {
            get { return _image; }

            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dob { get; set; }
        public DateTime DOB
        {

            get { return _dob; }

            set
            {
                _dob = value;
                OnPropertyChanged();
            }
        }

       

        public ProfileViewModel(string userId)
        {
            this.userId = userId;
            LoadUserData(userId);
        }

        public async void LoadUserData(string userId)
        {
            IsBusy = true;
            IsCheckedM = false;
            IsCheckedF = false;
            IsCheckedO = false;
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format(Constants.BaseUrl + "user/listuser/"+userId+"", string.Empty));
                var result = await client.GetStringAsync(uri);
                var UserList = JsonConvert.DeserializeObject<Users>(result);
                Name = UserList.USER_NAME;
                if (!(Name == null))
                {
                    string[] fullName = Name.Split(' ');
                    FirstName = fullName[0];
                    LastName = fullName[1];
                }
                Email = UserList.USER_EMAIL;
                DOB = Convert.ToDateTime(UserList.DATE_OF_BIRTH);
                Country = UserList.COUNTRY;
                Gender = (UserList.GENDER ?? "").ToString().Trim();
                Image = UserList.USER_PROFILE_IMAGE;
                if (Gender.Equals("M"))
                    IsCheckedM=true;
                else if(Gender.Equals("F"))
                    IsCheckedF = true;
                else
                    IsCheckedO = true;
                City = UserList.CITY;

                ProfileModel profileModel = new ProfileModel();
                profileModel.FirstName = UserList.USER_NAME;
                IsBusy = false;
            }
        }

       

        public ProfileViewModel(Page page)
        {

            _page = page;
            string UserId = Application.Current.Properties["USER_ID"].ToString();

            //Contact Name
            UpdateContactInCommand = new Command(async () => await UpdateContactAsync(UserId));

            //Date
            UpdateDateInCommand = new Command(async () => await UpdateDateAsync(DOB, UserId));

            //Gender
            //  UpdateGenderInCommand = new Command(async () => await UpdateGenderAsync(Gender, UserId));

            LoadUserData(UserId);
         
        }


        public async Task UpdateContactAsync(string userId)
        {
            ProfileModel.FirstName = FirstName + " ";
            ProfileModel.LastName = LastName;
            await _apiServices.UpdateContactAsync(ProfileModel, userId);
        }


        public async Task UpdateGender(string item, string userId)
        {
            await _apiServices.UpdateGenderAsync(item,userId);
        }

        /*public async void UpdatePhoto(string imagePath, string userId)
        {
            await _apiServices.UpdatePhotoAsync(imagePath, userId);

            await PopupNavigation.Instance.PopAsync();
        }*/


       
        //DATE
        private async Task UpdateDateAsync(DateTime dob,string userId)
        {
            await _apiServices.UpdateDateAsync(dob.ToString("yyyy-MM-dd"), userId);
            MessagingCenter.Send<App>((App)Application.Current, "OnCategoryCreated");
            await PopupNavigation.Instance.PopAsync();

           
            //  IsBusy = false;

        }


    }
}
