using System;
using Xamarin.Forms;

namespace NakayokunaruHandsOn
{
	public partial class RoundedBoxViewPage : ContentPage
	{
		public RoundedBoxViewPage()
		{
			InitializeComponent();
		}

		Random random = new Random ();

		private void OnClicked (object sender, EventArgs e)
		{
			roundedBox.Color = Color.FromRgb (
				random.Next (255),
				random.Next (255),
				random.Next (255));
		}
	}
}
