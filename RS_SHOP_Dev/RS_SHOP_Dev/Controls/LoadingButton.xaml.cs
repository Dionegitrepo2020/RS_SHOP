using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingButton 
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand),
            typeof(LoadingButton));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
            typeof(object),
            typeof(LoadingButton));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(LoadingButton), null,
            propertyChanged: (bindable, oldVal, newVal) => ((LoadingButton)bindable).OnTextChange((string)newVal));

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool),
            typeof(LoadingButton), false,
            propertyChanged: (bindable, oldVal, newVal) => ((LoadingButton)bindable).OnIsBusy((bool)newVal));

        public event EventHandler Clicked;

        public LoadingButton()
        {
            InitializeComponent();
            GetButtonStyle();
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        private void OnTextChange(string value)
        {
            InnerButton.Text = value;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);

            if (Command == null || !Command.CanExecute(CommandParameter))
                return;

            Command.Execute(CommandParameter);
        }

        private async void OnIsBusy(bool value)
        {
            if (value)
            {
                InnerActivityView.IsVisible = true;
                await InnerActivityView.FadeTo(1);
            }
            else
            {
                await InnerActivityView.FadeTo(0);
                InnerActivityView.IsVisible = false;
            }

            InnerActivityIndicator.IsRunning = value;
        }

        private void GetButtonStyle()
        {
            InnerBoxView.WidthRequest = InnerButton.Width;
            InnerBoxView.HeightRequest = InnerButton.Height;
            InnerBoxView.CornerRadius = InnerButton.CornerRadius;
            InnerBoxView.BackgroundColor = InnerButton.BackgroundColor;
            InnerBoxView.BorderThickness = (int)InnerButton.BorderWidth;
            InnerBoxView.BorderColor = InnerButton.BorderColor;
            InnerActivityIndicator.Color = InnerButton.TextColor;
        }
    }
}