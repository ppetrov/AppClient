using Xamarin.Forms;

namespace AppClient.Core.Navigation
{
	public sealed class PopUpSettings
	{
		public static readonly PopUpSettings Default = new PopUpSettings();

		public bool LightDismiss { get; }
		public double BackgroundOpacity { get; } = 0.5D;
		public LayoutOptions HorizontalOptions { get; } = LayoutOptions.Center;
		public LayoutOptions VerticalOptions { get; } = LayoutOptions.Center;
		public Thickness Margin { get; } = new Thickness(0);

		private PopUpSettings()
		{
		}

		public PopUpSettings(LayoutOptions horizontalOptions, LayoutOptions verticalOptions, Thickness margin)
		{
			this.HorizontalOptions = horizontalOptions;
			this.VerticalOptions = verticalOptions;
			this.Margin = margin;
		}

		public PopUpSettings(bool lightDismiss, double backgroundOpacity, LayoutOptions horizontalOptions, LayoutOptions verticalOptions, Thickness margin)
		{
			this.LightDismiss = lightDismiss;
			this.BackgroundOpacity = backgroundOpacity;
			this.HorizontalOptions = horizontalOptions;
			this.VerticalOptions = verticalOptions;
			this.Margin = margin;
		}

		public static PopUpSettings ForLightDismiss(LayoutOptions horizontalOptions, LayoutOptions verticalOptions, Thickness margin)
		{
			return new PopUpSettings(true, 0, horizontalOptions, verticalOptions, margin);
		}
	}
}