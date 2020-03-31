using Plugin.Connectivity;
using RS_SHOP_Dev.Helpers;
using RS_SHOP_Dev.Models.OrderModels;
using RS_SHOP_Dev.Resources;
using RS_SHOP_Dev.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RS_SHOP_Dev.ViewModels
{
    public class OrderViewModel: AcivityIndicatorHelper
    {
        private readonly OrderApiService _apiServices = new OrderApiService();

        List<OrderModel> _ordersList;
        public List<OrderModel> OrdersList
        {
            get
            {
                return _ordersList;
            }
            set
            {
                _ordersList = value;
                OnPropertyChanged();
            }
        }

        string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        string _orderId;
        public string OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged("OrderId");
            }
        }
        string _orderQR;
        public string OrderQR
        {
            get { return _orderQR; }
            set
            {
                _orderQR = value;
                OnPropertyChanged("OrderQR");
            }
        }
        bool _isFD;
        public bool isFD
        {
            get { return _isFD; }
            set
            {
                _isFD = value;
                OnPropertyChanged("isFD");
            }
        }
        string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        public OrderViewModel() { }
        
        public async Task LoadOrderProducts(string userId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var OrderProducts = await _apiServices.GetFromOrderService(userId);
                OrdersList = new List<OrderModel>(OrderProducts);
            }
        }

        public async Task LoadOrderByOrderId(string order_Id, string categ_Id)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var OrderProducts = await _apiServices.GetOrderByIdService(order_Id);
                OrdersList = new List<OrderModel>(OrderProducts);
                foreach (var order in OrdersList)
                {
                    Date = order.orderm.CREATED_DATE.Date.ToString("yyyy-MM-dd");
                    Time = order.orderm.CREATED_DATE.ToString("h:mm tt");
                    Name = order.orderm.USER_ID.ToString();
                    OrderId = order.orderm.ORDER_ID.ToString();
                    OrderQR = order.orderm.ORDER_QR_TAG.ToString();
                }
                if (categ_Id.Equals("10"))
                    isFD = true;
                else
                    isFD = false;
            }
        }
    }
}
