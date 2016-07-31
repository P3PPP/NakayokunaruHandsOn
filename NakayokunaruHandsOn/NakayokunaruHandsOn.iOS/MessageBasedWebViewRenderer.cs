using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.MessageBasedWebView), typeof(NakayokunaruHandsOn.iOS.MessageBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.iOS
{
	public class MessageBasedWebViewRenderer : ViewRenderer<MessageBasedWebView, UIWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<MessageBasedWebView> e)
		{
			if (Control == null)
			{
				var nativeControl = new UIWebView();
				nativeControl.LoadRequest(new NSUrlRequest(new NSUrl("http://ticktack.hatenablog.jp/entry/2016/06/11/124751")));
				SetNativeControl(nativeControl);
			}

			if (e.OldElement != null)
			{
				MessagingCenter.Unsubscribe<MessageBasedWebView>(this, MessageBasedWebView.GoBackKey);
				MessagingCenter.Unsubscribe<MessageBasedWebView>(this, MessageBasedWebView.GoForwardKey);
				MessagingCenter.Unsubscribe<MessageBasedWebView, string>(this, MessageBasedWebView.EvalKey);
			}

			if (e.NewElement != null)
			{
				MessagingCenter.Subscribe<MessageBasedWebView>(
					this,
					MessageBasedWebView.GoBackKey,
					_ => OnGoBackRequested(),
					e.NewElement);
				MessagingCenter.Subscribe<MessageBasedWebView>(
					this,
					MessageBasedWebView.GoForwardKey,
					_ => OnGoForwardRequested(),
					e.NewElement);
				MessagingCenter.Subscribe<MessageBasedWebView, string>(
					this,
					MessageBasedWebView.EvalKey,
					(sender, args) => OnEvalRequested(args),
					e.NewElement);
			}

			base.OnElementChanged(e);
		}

		private void OnGoBackRequested()
		{
			if (Control.CanGoBack)
			{
				Control.GoBack();
			}
		}

		private void OnGoForwardRequested()
		{
			if (Control.CanGoForward)
			{
				Control.GoForward();
			}
		}

		private void OnEvalRequested(string script)
		{
			Control.EvaluateJavascript(script);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			Control.Frame = this.Bounds;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				MessagingCenter.Unsubscribe<MessageBasedWebView>(this, MessageBasedWebView.GoBackKey);
				MessagingCenter.Unsubscribe<MessageBasedWebView>(this, MessageBasedWebView.GoForwardKey);
				MessagingCenter.Unsubscribe<MessageBasedWebView, string>(this, MessageBasedWebView.EvalKey);
			}

			base.Dispose(disposing);
		}
	}
}

