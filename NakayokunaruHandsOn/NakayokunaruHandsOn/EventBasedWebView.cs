using System;
using Xamarin.Forms;

namespace NakayokunaruHandsOn
{
	public class EventBasedWebView : View
	{
		public void GoBack()
		{
			EventHandler handler = GoBackRequested;
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		public void GoForward()
		{
			EventHandler handler = GoForwardRequested;
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		public void Eval(string script)
		{
			EventHandler<EvalRequestedEventArgs> handler = EvalRequested;
			if (handler != null)
			{
				handler.Invoke(this, new EvalRequestedEventArgs(script));
			}
		}

		internal event EventHandler GoBackRequested;
		internal event EventHandler GoForwardRequested;
		internal event EventHandler<EvalRequestedEventArgs> EvalRequested;
	}

	public class EvalRequestedEventArgs
	{
		public string Script
		{
			get;
			private set;
		}

		public EvalRequestedEventArgs(string script)
		{
			Script = script;
		}
	}
}

