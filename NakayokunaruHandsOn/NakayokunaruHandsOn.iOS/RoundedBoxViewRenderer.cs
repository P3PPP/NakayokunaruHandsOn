using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.RoundedBoxView), typeof(NakayokunaruHandsOn.iOS.RoundedBoxViewRenderer))]

namespace NakayokunaruHandsOn.iOS
{
	public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, UIView>
	{
		private UITapGestureRecognizer tapGesuture;

		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
		{
			if (Control == null)
			{
				var nativeControl = new UIView();
				tapGesuture = new UITapGestureRecognizer(() => Element?.SendClick());
				nativeControl.AddGestureRecognizer(tapGesuture);
				SetNativeControl(nativeControl);
			}

			if (e.OldElement != null)
			{

			}

			if (e.NewElement != null)
			{
				// Forms�R���g���[���̃v���p�e�B�l�𔽉f
				UpdateRadius();
				UpdateColor();
			}

			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			// �v���p�e�B�l�̕ύX�𔽉f
			if (e.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
			{
				UpdateRadius();
			}
			if (e.PropertyName == RoundedBoxView.ColorProperty.PropertyName)
			{
				UpdateColor();
			}
		}

		private void UpdateRadius()
		{
			Control.Layer.CornerRadius = (float)Element.CornerRadius;
		}

		private void UpdateColor()
		{
			Control.BackgroundColor = Element.Color.ToUIColor();
		}

		protected override void Dispose(bool disposing)
		{
			if (Control != null)
				Control.RemoveGestureRecognizer(tapGesuture);

			base.Dispose(disposing);
		}
	}
}

