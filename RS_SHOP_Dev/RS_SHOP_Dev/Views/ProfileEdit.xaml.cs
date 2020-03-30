using Plugin.Media;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using RS_SHOP_Dev.Views.PopupViews;
using System;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RS_SHOP_Dev.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEdit : ContentPage
    {
        string UserId;
        // Image image;

        public ProfileEdit()
        {
            InitializeComponent();
            UserId = (Application.Current.Properties["USER_ID"] ?? "0").ToString();
            this.BindingContext = new ProfileViewModel(UserId);
            MessagingCenter.Subscribe<App>((App)Application.Current, "OnCategoryCreated", (sender) =>
            {
                OnAppearing();
            });
        }

        protected async override void OnAppearing()
        {
            (this.BindingContext as ProfileViewModel).LoadUserData(UserId);
        }

        private async void PhotoEdit(object sender, EventArgs e)
        {
           // await PopupNavigation.Instance.PushAsync(new EditCountryPop());
              await PickPhotoFromGallery();

        }

        private async void CameraEdit(object sender, EventArgs e)
        {

              await TakePhotoFromCameraAsync();

        }

        private async Task TakePhotoFromCameraAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.GetStream()), "\"file\"", $"\"{file.Path}\"");

            var httpClient = new System.Net.Http.HttpClient();
            var url = "http://dionesql.southindia.cloudapp.azure.com/Rsshop1/api/user/save/" + Application.Current.Properties["USER_ID"].ToString();
            var responseMsg = await httpClient.PostAsync(url, content);

            var remotePath = responseMsg.Content.ReadAsStringAsync();

            //  await DisplayAlert("File Location", file.Path, "OK");


            FileImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async Task PickPhotoFromGallery()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });



            if (file == null)
                return;
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.GetStream()), "\"file\"", $"\"{file.Path}\"");

            var httpClient = new System.Net.Http.HttpClient();
            var url = "http://dionesql.southindia.cloudapp.azure.com/Rsshop1/api/user/save/" + Application.Current.Properties["USER_ID"].ToString();
            var responseMsg = await httpClient.PostAsync(url, content);

            var remotePath = responseMsg.Content.ReadAsStringAsync();

            FileImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

        }


        private async void ContactEdit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditNamePop());

        }

        private async void CountryEdit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditCountryPop());
        }

        private async void GenderEdit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditGenderPop());
        }

        private async void BirthdayEdit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new EditBirthdayPop());
        }


    }


}