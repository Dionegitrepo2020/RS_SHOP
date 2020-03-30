using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.ShipAddressModel;
using RS_SHOP_Dev.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class AddressViewModel : AcivityIndicatorHelper
    {
        private readonly ShipAddApiService _apiServicesAdd = new ShipAddApiService();
        public ShipAddressModel shipAddressModel { get; set; } = new ShipAddressModel();
        private Page _page;

        public ICommand SaveShipAddress { get; }

        string _contactName;
        public string ContactName
        {
            get
            {
                return _contactName;
            }
            set
            {
                _contactName = value;
                OnPropertyChanged("ContactName");
            }
        }



        string _address1;
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
                OnPropertyChanged("Address1");
            }
        }



        string _address2;
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
                OnPropertyChanged("Address2");
            }
        }
        string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }

        string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        string _postCode;
        public string PostCode
        {
            get
            {
                return _postCode;
            }
            set
            {
                _postCode = value;
                OnPropertyChanged("PostCode");
            }
        }


        string _userId;
        public string UserID
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                OnPropertyChanged("UserID");
            }
        }

        private bool _isTrue;
        public bool IsTrue
        {
            get { return _isTrue; }
            set
            {
                _isTrue = value;
                OnPropertyChanged("IsTrue");
            }
        }

        List<ShipAddressModel> _shipAddressList;
        public List<ShipAddressModel> ShipAddressList
        {
            get
            {
                return _shipAddressList;
            }
            set
            {
                _shipAddressList = value;
                OnPropertyChanged();
            }
        }


        public AddressViewModel()
        {
            SaveShipAddress = new Command(async () => await SaveShipAddressAsync());

        }

        public async Task RemoveAddressData(int address_ID)
        {
            ShipAddressList = await _apiServicesAdd.RemoveAddressAsync(address_ID);
        }


        private async Task SaveShipAddressAsync()
        {
            shipAddressModel.FULL_NAME = ContactName;
            shipAddressModel.ADDRESS1 = Address1;
            shipAddressModel.ADDRESS2 = Address2;
            shipAddressModel.COUNTRY = Country;
            shipAddressModel.POST_CODE = PostCode;
            shipAddressModel.CITY = City;
            shipAddressModel.PHONE = Phone;
            shipAddressModel.IS_DEFAULT = IsTrue;

            var ResultContent = await _apiServicesAdd.SaveShipAddAsync(shipAddressModel);
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(ResultContent);
            var Message = jwtDynamic.Value<string>("Message");
            if (Message.Equals("Address Saved."))
                MessagingCenter.Send<App>((App)Application.Current, "OnCategoryCreated");
            await PopupNavigation.Instance.PopAsync();
        }

        //
        public async Task LoadAddressList(string userId)
        {
            ShipAddressList = await _apiServicesAdd.LoadShipAddAsync(userId);
        }

        public void LoadAddressOnPopup(ShipAddressModel shipAddressModel)
        {
            ContactName = shipAddressModel.FULL_NAME;
            Address1 = shipAddressModel.ADDRESS1;
            Address2 = shipAddressModel.ADDRESS2;
            Country = shipAddressModel.COUNTRY;
            PostCode = shipAddressModel.POST_CODE;
            City = shipAddressModel.CITY;
            Phone = shipAddressModel.PHONE;
            IsTrue = shipAddressModel.IS_DEFAULT;

        }


    }

}
