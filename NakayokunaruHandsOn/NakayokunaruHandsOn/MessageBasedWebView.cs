using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NakayokunaruHandsOn
{
	public class MessageBasedWebView : View
	{
		internal static readonly string GoBackKey = "MessageBasedWebView.GoBack";
		internal static readonly string GoForwardKey = "MessageBasedWebView.GoForward";
		internal static readonly string EvalKey = "MessageBasedWebView.Eval";

		public void GoBack()
		{
			MessagingCenter.Send(this, GoBackKey);
		}

		public void GoForward()
		{
			MessagingCenter.Send(this, GoForwardKey);
		}

		public void Eval(string script)
		{
			MessagingCenter.Send(this, EvalKey, script);
		}
	}
}

