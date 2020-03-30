using Plugin.Connectivity;
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
    public partial class Home : TabbedPage
    {
        private int tabIndex=0;
        string UserId;
        public Home(int v)
        {
            InitializeComponent();
            this.tabIndex = v;
            UserId = (Application.Current.Properties["USER_ID"]??"0").ToString();
            var pages = Children.GetEnumerator();
            for (int i = 0; i <= this.tabIndex; i++)
            {
                pages.MoveNext();
            }
            CurrentPage = pages.Current;
        }
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            int index = Children.IndexOf(CurrentPage);

            if (index == 1|| index==3 || index==4)
            {
                if (UserId == "0")
                {
                    PopupNavigation.Instance.PushAsync(new AuthAlert());
                }
            }

            else if (index == 2)
            {
                MessagingCenter.Send<Object>(this, "click_third_tab");
            }


        }
    }
}