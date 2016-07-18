using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.EventBasedWebView), typeof(NakayokunaruHandsOn.UWP.EventBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.UWP
{
	public class EventBasedWebViewRenderer : ViewRenderer<EventBasedWebView, Windows.UI.Xaml.Controls.WebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<EventBasedWebView> e)
		{
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var nativeControl = new Windows.UI.Xaml.Controls.WebView();
					nativeControl.Source = new Uri("http://ticktack.hatenablog.jp/entry/2016/06/11/124751");
					SetNativeControl(nativeControl);
				}
				e.NewElement.GoBackRequested += OnGoBackRequested;
				e.NewElement.GoForwardRequested += OnGoForwardRequested;
				e.NewElement.EvalRequested += OnEvalRequested;
			}

			if (e.OldElement != null)
			{
				e.OldElement.GoBackRequested -= OnGoBackRequested;
				e.OldElement.GoForwardRequested -= OnGoForwardRequested;
				e.OldElement.EvalRequested -= OnEvalRequested;
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

		private async void OnEvalRequested(object sender, EvalRequestedEventArgs e)
		{
			await Control.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				async () => await Control.InvokeScriptAsync("eval", new[] { e.Script }));
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
