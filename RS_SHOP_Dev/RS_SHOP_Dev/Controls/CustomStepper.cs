using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RS_SHOP_Dev.Controls
{
    public class CustomStepper : StackLayout
    {
        Button PlusBtn;
        Button MinusBtn;
        Label Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(CustomStepper),
              defaultValue: 1,
              defaultBindingMode: BindingMode.TwoWay);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public CustomStepper()
        {
            MinusBtn = new Button { Text = "-", WidthRequest = 30, HeightRequest=30,Padding=-6, FontAttributes = FontAttributes.Bold, FontSize = 30, CornerRadius=40,TextColor=Color.FromHex("#00c3ff") };
            PlusBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30, Padding = -6, FontAttributes = FontAttributes.Bold, FontSize = 30, CornerRadius = 40, TextColor = Color.FromHex("#00c3ff") };
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        PlusBtn.BackgroundColor = Color.FromHex("#78dfff");
                        MinusBtn.BackgroundColor = Color.FromHex("#78dfff");
                        break;
                    }
                case Device.iOS:
                    {
                        PlusBtn.BackgroundColor = Color.FromHex("#78dfff");
                        MinusBtn.BackgroundColor = Color.FromHex("#78dfff");
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            Spacing = 0;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;
            Entry = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                WidthRequest=25,
                TextColor = Color.FromHex("#00c3ff"),
            };
            Entry.SetBinding(Label.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            //Entry.TextChanged += Entry_TextChanged;
            Children.Add(MinusBtn);
            Children.Add(Entry);
            Children.Add(PlusBtn);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                this.Text = int.Parse(e.NewTextValue);
            if (Text > 5)
                Text = 5;
            if (Text < 1)
                Text = 1;
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > 1)
                Text--;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if(Text<5)
            Text++;
        }

    }
}
