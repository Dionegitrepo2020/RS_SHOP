using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RS_SHOP_Dev.Helpers
{
    public class AcivityIndicatorHelper : INotifyPropertyChanged
    {
        //ActivityIndicator for async Task
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        //
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        string isStored;
        public string IsStored
        {
            get { return isStored; }
            set
            {
                isStored = value;
                OnPropertyChanged("IsStored");
            }
        }
        private string _match_id;
        public string match_id
        {
            get { return _match_id; }
            set
            {
                _match_id = value;
                OnPropertyChanged("match_id");
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
