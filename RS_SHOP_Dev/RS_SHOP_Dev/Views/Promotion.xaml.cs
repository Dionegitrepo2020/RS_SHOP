using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
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
    public partial class Promotion : TabbedPage
    {
        string User_Id;
        public Promotion()
        {
            InitializeComponent();
            User_Id = (Application.Current.Properties["USER_ID"]??"0").ToString();
        }
    }
}