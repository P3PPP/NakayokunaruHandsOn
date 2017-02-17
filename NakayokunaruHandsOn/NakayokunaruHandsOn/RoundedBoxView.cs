using System;
using Xamarin.Forms;

namespace NakayokunaruHandsOn
{
	public class RoundedBoxView : View
	{
		#region CornerRadius BindableProperty
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(RoundedBoxView), 5.0);

		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		#endregion

		#region Color BindableProperty
		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(Color), typeof(Color), typeof(RoundedBoxView), Color.Accent);

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}
		#endregion

		public event EventHandler Clicked;

		internal void SendClick()
		{
			Clicked?.Invoke(this, EventArgs.Empty);
		}
	}
}

