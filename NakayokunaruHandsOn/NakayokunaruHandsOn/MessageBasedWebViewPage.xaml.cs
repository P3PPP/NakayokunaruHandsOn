using Xamarin.Forms;

namespace NakayokunaruHandsOn
{
	public partial class MessageBasedWebViewPage : ContentPage
	{
		public MessageBasedWebViewPage()
		{
			InitializeComponent();
		}

		void GoBackClicked(object sender, System.EventArgs e)
		{
			webView.GoBack();
		}

		void GoForwardClicked(object sender, System.EventArgs e)
		{
			webView.GoForward();
		}

		void EvalClicked(object sender, System.EventArgs e)
		{
			webView.Eval(entry.Text);
		}
	}
}
