using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

[assembly: ExportRenderer(typeof(NakayokunaruHandsOn.RoundedBoxView), typeof(NakayokunaruHandsOn.UWP.RoundedBoxViewRenderer))]


namespace NakayokunaruHandsOn.UWP
{
	public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, Windows.UI.Xaml.Shapes.Rectangle>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var nativeControl = new Windows.UI.Xaml.Shapes.Rectangle();
					nativeControl.DataContext = Element;
					nativeControl.SetBinding(Shape.FillProperty,
						new Windows.UI.Xaml.Data.Binding
						{
							Converter = new ColorConverter(),
							Path = new PropertyPath(NakayokunaruHandsOn.RoundedBoxView.ColorProperty.PropertyName),
						});
					nativeControl.SetBinding(Windows.UI.Xaml.Shapes.Rectangle.RadiusXProperty,
						new Windows.UI.Xaml.Data.Binding
						{
							Path = new PropertyPath(NakayokunaruHandsOn.RoundedBoxView.CornerRadiusProperty.PropertyName),
						});
					nativeControl.SetBinding(Windows.UI.Xaml.Shapes.Rectangle.RadiusYProperty,
						new Windows.UI.Xaml.Data.Binding
						{
							Path = new PropertyPath(NakayokunaruHandsOn.RoundedBoxView.CornerRadiusProperty.PropertyName),
						});
					nativeControl.Tapped += (sender, args) => Element.SendClick();
					SetNativeControl(nativeControl);
				}
			}
		}
	}
}
