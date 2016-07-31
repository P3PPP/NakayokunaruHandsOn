using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.EventBasedWebView), typeof(NakayokunaruHandsOn.iOS.EventBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.iOS
{
	public class EventBasedWebViewRenderer : ViewRenderer<EventBasedWebView, UIWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<EventBasedWebView> e)
		{
			if (Control == null)
			{
				var nativeControl = new UIWebView();
				nativeControl.LoadRequest(new NSUrlRequest(new NSUrl("http://ticktack.hatenablog.jp/entry/2016/06/11/124751")));
				SetNativeControl(nativeControl);
			}

			if (e.OldElement != null)
			{
				Element.GoBackRequested -= OnGoBackRequested;
				Element.GoForwardRequested -= OnGoForwardRequested;
				Element.EvalRequested -= OnEvalRequested;
			}

			if (e.NewElement != null)
			{
				Element.GoBackRequested += OnGoBackRequested;
				Element.GoForwardRequested += OnGoForwardRequested;
				Element.EvalRequested += OnEvalRequested;
			}

			base.OnElementChanged(e);
		}

		private void OnGoBackRequested(object sender, EventArgs e)
		{
			if (Control.CanGoBack)
			{
				Control.GoBack();
			}
		}

		private void OnGoForwardRequested(object sender, EventArgs e)
		{
			if (Control.CanGoForward)
			{
				Control.GoForward();
			}
		}

		private void OnEvalRequested(object sender, EvalRequestedEventArgs e)
		{
			Control.EvaluateJavascript(e.Script);
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
				Element.GoBackRequested -= OnGoBackRequested;
				Element.GoForwardRequested -= OnGoForwardRequested;
				Element.EvalRequested -= OnEvalRequested;
			}

			base.Dispose(disposing);
		}
	}
}

