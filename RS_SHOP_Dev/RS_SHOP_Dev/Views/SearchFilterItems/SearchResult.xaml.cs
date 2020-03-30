using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.SearchFilterItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResult : ContentPage
    {
        string ProdID;
        public SearchResult(int pRODUCT_ID)
        {
            InitializeComponent();
            ProdID = pRODUCT_ID.ToString();
        }
    }
}