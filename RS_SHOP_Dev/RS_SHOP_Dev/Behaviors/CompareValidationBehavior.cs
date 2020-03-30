using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RS_SHOP_Dev.Behaviors
{
    public class CompareValidationBehavior : Behavior<Entry>
    {
        public static BindableProperty TextProperty = BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = e.NewTextValue == Text;

            /*if (IsValid == true)
            {
                ((Entry)sender).BackgroundColor = IsValid ? Color.White : Color.White;
            }
            else
            {
                ((Entry)sender).BackgroundColor = IsValid ? Color.White : Color.Red;
            }*/

            ((Entry)sender).TextColor = IsValid ? Color.SkyBlue : Color.Red;

        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
