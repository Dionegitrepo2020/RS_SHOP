﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using RS_SHOP_Dev;
using RS_SHOP_Dev.iOS;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using RS_SHOP_Dev.CustomRenderers;
using CoreAnimation;
using System.Drawing;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace RS_SHOP_Dev.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || e.NewElement == null)
				return;

			var element = (CustomEntry)this.Element;
			var textField = this.Control;
			if (!string.IsNullOrEmpty(element.Image))
			{
				switch (element.ImageAlignment)
				{
					case ImageAlignment.Left:
						textField.LeftViewMode = UITextFieldViewMode.Always;
						textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
						break;
					case ImageAlignment.Right:
						textField.RightViewMode = UITextFieldViewMode.Always;
						textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
						break;
				}
			}

			textField.BorderStyle = UITextBorderStyle.None;
			CALayer bottomBorder = new CALayer
			{
				Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
				BorderWidth = 2.0f,
				BorderColor = element.LineColor.ToCGColor()
			};

			textField.Layer.AddSublayer(bottomBorder);
			textField.Layer.MasksToBounds = true;
		}

		private UIView GetImageView(string imagePath, int height, int width)
		{
			var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
			{
				Frame = new RectangleF(0, 0, width, height)
			};
			UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
			objLeftView.AddSubview(uiImageView);

			return objLeftView;
		}
	}
}