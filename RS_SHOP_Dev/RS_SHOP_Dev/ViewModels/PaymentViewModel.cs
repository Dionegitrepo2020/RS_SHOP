using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.OrderModels;
using RS_SHOP_Dev.Models.PaymentModel;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RS_SHOP_Dev.ViewModels
{
    public class PaymentViewModel : AcivityIndicatorHelper
    {
        private readonly PayApiService _apiServices = new PayApiService();


        string _number;
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        string _expire;
        public string Expdate
        {
            get
            {
                return _expire;
            }
            set
            {
                _expire = value;
                OnPropertyChanged("Expdate");
            }
        }

        string _cNumber;
        public string CNumber
        {
            get
            {
                return _cNumber;
            }
            set
            {
                _cNumber = value;
                OnPropertyChanged("CNumber");
            }
        }
        string _chName;
        public string Chname
        {
            get
            {
                return _chName;
            }
            set
            {
                _chName = value;
                OnPropertyChanged("Chname");
            }
        }

        Month _month;
        public Month Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }
        string _selectedmonth;
        public string SelectedMonth
        {
            get
            {
                return _selectedmonth;
            }
            set
            {
                _selectedmonth = value;
                OnPropertyChanged("SelectedMonth");
            }
        }
        string _selectedyear;
        public string SelectedYear
        {
            get
            {
                return _selectedyear;
            }
            set
            {
                _selectedyear = value;
                OnPropertyChanged("SelectedYear");
            }
        }

        Year _year;
        public Year Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        string _orderId;
        public string OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                _orderId = value;
                OnPropertyChanged("OrderId");
            }
        }
        string _amount;
        public string AMOUNT
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("AMOUNT");
            }
        }
        string _dateTime;
        public string DATETIME
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DATETIME");
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
        bool _isSaveCard;
        public bool IsSaveCard
        {
            get
            {
                return _isSaveCard;
            }
            set
            {
                _isSaveCard = value;
                OnPropertyChanged("IsSaveCard");
            }
        }
        private readonly PaymentApiService _apiServicesPayment = new PaymentApiService();
        public PaymentListModel paymentListModel { get; set; } = new PaymentListModel();
        private Page _page;
        public ICommand SaveCard { get; }

        public List<Month> MonthList { get; set; }

        public List<Year> YearList { get; set; }

        List<PaymentListModel> _paymentList;
        public List<PaymentListModel> PaymentList
        {
            get
            {
                return _paymentList;
            }
            set
            {
                _paymentList = value;
                OnPropertyChanged();
            }
        }
        public PaymentViewModel()
        {
            SaveCard = new Command(async () => await SaveCardAsync());
            
            MonthList = GetMonths().OrderBy(t => t.Value).ToList();
            YearList = GetYears().OrderBy(t => t.Value).ToList();
        }

        public async Task RemoveCardData(int cARD_ID)
        {
            if (CrossConnectivity.Current.IsConnected)
                PaymentList = await _apiServicesPayment.RemoveCardAsync(cARD_ID);
        }

        private async Task SaveCardAsync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                paymentListModel.CARD_NUMBER = Convert.ToInt64(Number);
                paymentListModel.CARD_HOLDER_NAME = Chname;
                paymentListModel.CARD_EXP_DATE = SelectedMonth + SelectedYear.Substring(2, 2);
                var ResultContent = await _apiServicesPayment.SaveCardAsync(paymentListModel);
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(ResultContent);
                var Message = jwtDynamic.Value<string>("Message");
                if (Message.Equals("Card Saved."))
                    await PopupNavigation.Instance.PopAsync();
            }
        }

        public async Task LoadCards(string userId)
        {
            if (CrossConnectivity.Current.IsConnected)
                PaymentList = await _apiServicesPayment.LoadCardAsync(userId);
        }
        public void LoadCardsOnPopup(PaymentListModel paymentListModel)
        {
            Number = (paymentListModel.CARD_NUMBER??"").ToString();
            Chname = paymentListModel.CARD_HOLDER_NAME;
            SelectedMonth = paymentListModel.CARD_EXP_DATE;
            if (!(SelectedMonth == null))
                SelectedMonth = SelectedMonth.Substring(0,2);
            SelectedYear = paymentListModel.CARD_EXP_DATE;
            if (!(SelectedYear == null))
                SelectedYear = "20"+SelectedYear.Substring(2, 2);
        }

        public void LoadScreen(PaymentListModel paymentModel)
        {
            Number = System.Text.RegularExpressions.Regex.Replace(paymentModel.CARD_NUMBER.ToString(), ".{4}", "$0-").Remove(19);
            Expdate = System.Text.RegularExpressions.Regex.Replace(paymentModel.CARD_EXP_DATE, ".{2}", "$0/");
            Chname = paymentModel.CARD_HOLDER_NAME;
            IsSaveCard = paymentModel.CARD_DEFAULT==null ? true : false;
        }

        public async Task PaymentRequest(List<OrderResponse> orderResponses, string categ_Id)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IsBusy = true;
                if (IsSaveCard.Equals(true))
                {
                    paymentListModel.CARD_NUMBER = Convert.ToInt64(Number.Replace("-", ""));
                    paymentListModel.CARD_HOLDER_NAME = Chname;
                    paymentListModel.CARD_EXP_DATE = Expdate.Replace("/", "");
                    var save = await _apiServicesPayment.SaveCardAsync(paymentListModel);
                }

                foreach (var order in orderResponses)
                {
                    UserID = order.USER_ID.ToString();
                    OrderId = order.ORDER_REF_NO.ToString();
                    AMOUNT = order.ORDER_AMOUNT.ToString();
                    DATETIME = order.CREATED_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                }
                JObject jObject = new JObject();
                jObject.Add("USER_ID", UserID);
                jObject.Add("ORDER_REF_NO", OrderId);
                jObject.Add("CARD_NUMBER", Number.Replace("-", ""));
                jObject.Add("EXPIRES", Expdate.Replace("/", ""));
                jObject.Add("CVV_NUM", CNumber);
                jObject.Add("CH_NAME", Chname);
                jObject.Add("AMOUNT", Convert.ToInt64(Convert.ToDecimal(AMOUNT) * 100));
                jObject.Add("DATE_TIME", DATETIME);
                var ResultContent = await _apiServices.ProcessPayment(jObject);
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(ResultContent);
                var ResultStatus = jwtDynamic.Value<string>("STATUS");
                var ResultMessage = jwtDynamic.Value<string>("Message");
                IsBusy = false;
                if (ResultStatus.Equals("00"))
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                    var Order_Id = jwtDynamic.Value<string>("ORDER_ID");
                    await PopupNavigation.Instance.PushAsync(new OrderPopUp(Order_Id, categ_Id));
                }
                else if (ResultStatus.Equals("101"))
                {
                    await PopupNavigation.Instance.PushAsync(new LoginAlert(ResultMessage));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new LoginAlert(ResultMessage));
                }
            }
        }

        public List<Month> GetMonths()
        {
            var months = new List<Month>()
            {
               new Month(){Key = 0, Value = "Select"},
               new Month(){Key = 1, Value = "01"},
               new Month(){Key = 2, Value = "02"},
               new Month(){Key = 3, Value = "03"},
               new Month(){Key = 4, Value = "04"},
               new Month(){Key = 5, Value = "05"},
               new Month(){Key = 6, Value = "06"},
               new Month(){Key = 7, Value = "07"},
               new Month(){Key = 8, Value = "08"},
               new Month(){Key = 9, Value = "09"},
               new Month(){Key = 10, Value = "10"},
               new Month(){Key = 11, Value = "11"},
               new Month(){Key = 12, Value = "12"}
            };
            return months;
        }


        public List<Year> GetYears()
        {
            var years = new List<Year>()
            {
               new Year(){Key = 0, Value = "Select"},
               new Year(){Key = 1, Value = "2015"},
               new Year(){Key = 2, Value = "2016"},
               new Year(){Key = 3, Value = "2017"},
               new Year(){Key = 4, Value = "2018"},
               new Year(){Key = 5, Value = "2019"},
               new Year(){Key = 6, Value = "2020"},
               new Year(){Key = 7, Value = "2021"},
               new Year(){Key = 8, Value = "2022"},
               new Year(){Key = 9, Value = "2023"},
               new Year(){Key = 10, Value = "2024"},
               new Year(){Key = 11, Value = "2025"},
               new Year(){Key = 12, Value = "2026"},
               new Year(){Key = 13, Value = "2027"},
               new Year(){Key = 14, Value = "2028"},
               new Year(){Key = 15, Value = "2029"},
               new Year(){Key = 16, Value = "2030"}
            };
            return years;
        }

    }

    public class Month
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

    public class Year
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}

