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

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.MessageBasedWebView), typeof(NakayokunaruHandsOn.UWP.MessageBasedWebViewRenderer))]

namespace NakayokunaruHandsOn.UWP
{
	public class MessageBasedWebViewRenderer : ViewRenderer<MessageBasedWebView, Windows.UI.Xaml.Controls.WebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<MessageBasedWebView> e)
		{
			if (Control == null)
			{
				var nativeControl = new Windows.UI.Xaml.Controls.WebView();
				nativeControl.Source = new Uri("http://ticktack.hatenablog.jp/entry/2016/06/11/124751");
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

		private async void OnEvalRequested(string script)
		{
			await Control.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				async () => await Control.InvokeScriptAsync("eval", new[] { script }));
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