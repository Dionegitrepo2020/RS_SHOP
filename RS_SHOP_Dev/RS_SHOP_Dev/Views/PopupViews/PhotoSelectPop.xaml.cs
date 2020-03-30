using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Views.PopupViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoSelectPop : PopupPage
    {
        public PhotoSelectPop()
        {
            InitializeComponent();
            this.BindingContext = new ProfileViewModel(this);


            takePhoto.Clicked += async (sender, args) =>
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

                await DisplayAlert("File Location", file.Path, "OK");

                FileImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };


            pickPhoto.Clicked += async (sender, args) =>
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

                await DisplayAlert("File Location", file.Path, "OK");

                FileImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };

        }


        private void Handle_Photo(object sender, EventArgs e)
        {
            //Popup Close
            PopupNavigation.Instance.PopAsync();
            //  Navigation.PushAsync(new ProfileEdit());
          //  UploadPhoto();

        }

        async void UploadPhoto()
        {
            
        }

        private void Handle_Photo_Cancel(object sender, EventArgs e)
        {
            //Popup Close
            PopupNavigation.Instance.PopAsync();
            //  Navigation.PushAsync(new ProfileEdit());
        }
    }
}