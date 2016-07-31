using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;
using Android.Webkit;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.MessageBasedWebView), typeof(NakayokunaruHandsOn.Droid.MessageBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.Droid
{
	public class MessageBasedWebViewRenderer : ViewRenderer<MessageBasedWebView, AWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<MessageBasedWebView> e)
		{
			if (Control == null)
			{
				var nativeControl = new AWebView(Context);
				// WebView内でリンクを開くために必要
				nativeControl.SetWebViewClient(new WebClient());
				// Eval確認でalert()を使うために必要
				nativeControl.SetWebChromeClient(new WebChromeClient());
				nativeControl.Settings.JavaScriptEnabled = true;
				nativeControl.LoadUrl("http://ticktack.hatenablog.jp/entry/2016/06/11/124751");
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
			if (Control.CanGoBack())
			{
				Control.GoBack();
			}
		}

		private void OnGoForwardRequested()
		{
			if (Control.CanGoForward())
			{
				Control.GoForward();
			}
		}

		private void OnEvalRequested(string script)
		{
			Control.LoadUrl("javascript:" + script);
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

		private class WebClient : WebViewClient
		{
			public override bool ShouldOverrideUrlLoading(AWebView view, string url)
			{
				view.LoadUrl(url);
				return true;
			}
		}
	}
}

