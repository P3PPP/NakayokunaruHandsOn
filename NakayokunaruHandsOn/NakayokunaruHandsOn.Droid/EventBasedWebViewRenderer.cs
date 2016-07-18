using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWebView = Android.Webkit.WebView;
using Android.Webkit;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.EventBasedWebView), typeof(NakayokunaruHandsOn.Droid.EventBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.Droid
{
	public class EventBasedWebViewRenderer : ViewRenderer<EventBasedWebView, AWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<EventBasedWebView> e)
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
			if (Control.CanGoBack())
			{
				Control.GoBack();
			}
		}

		private void OnGoForwardRequested(object sender, EventArgs e)
		{
			if (Control.CanGoForward())
			{
				Control.GoForward();
			}
		}

		private void OnEvalRequested(object sender, EvalRequestedEventArgs e)
		{
			Control.LoadUrl("javascript:" + e.Script);
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

