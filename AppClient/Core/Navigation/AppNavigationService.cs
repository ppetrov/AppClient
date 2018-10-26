using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppClient.Core.Navigation
{
	public sealed class AppNavigationService
	{
		/// <summary>
		/// Opens the page as a modal page
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public Task OpenModalAsync(ContentPage page)
		{
			if (page == null) throw new ArgumentNullException(nameof(page));

			return Application.Current.MainPage.Navigation.PushModalAsync(page);
		}

		/// <summary>
		/// Open the view as pop up with viewModel set as BindingContext
		/// </summary>
		/// <param name="view"></param>
		/// <param name="settings"></param>
		/// <returns></returns>
		public Task OpenPopUpAsync(ContentView view, PopUpSettings settings = null)
		{
			if (view == null) throw new ArgumentNullException(nameof(view));

			var page = GetCurrentContentPage();

			var grid = page.Content as Grid;

			// Solid Background
			var backgroundView = new ContentView();

			settings = settings ?? PopUpSettings.Default;

			if (settings.LightDismiss)
			{
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.Command = new Command(() => { this.ClosePopUpAsync(); });
				backgroundView.GestureRecognizers.Add(tapGestureRecognizer);
			}
			backgroundView.BackgroundColor = Color.Black;
			backgroundView.Opacity = settings.BackgroundOpacity;
			backgroundView.SetValue(Grid.RowProperty, 0);
			backgroundView.SetValue(Grid.ColumnProperty, 0);
			if (grid.RowDefinitions.Count > 0)
			{
				backgroundView.SetValue(Grid.RowSpanProperty, grid.RowDefinitions.Count);
			}
			if (grid.ColumnDefinitions.Count > 0)
			{
				backgroundView.SetValue(Grid.ColumnSpanProperty, grid.ColumnDefinitions.Count);
			}

			view.HorizontalOptions = settings.HorizontalOptions;
			view.VerticalOptions = settings.VerticalOptions;
			view.Margin = settings.Margin;
			view.SetValue(Grid.RowProperty, 0);
			view.SetValue(Grid.ColumnProperty, 0);
			if (grid.RowDefinitions.Count > 0)
			{
				view.SetValue(Grid.RowSpanProperty, grid.RowDefinitions.Count);
			}
			if (grid.ColumnDefinitions.Count > 0)
			{
				view.SetValue(Grid.ColumnSpanProperty, grid.ColumnDefinitions.Count);
			}

			grid.Children.Add(backgroundView);
			grid.Children.Add(view);

			return Task.CompletedTask;
		}

		/// <summary>
		/// Display the view in the host using an optional animation
		/// </summary>
		/// <param name="host"></param>
		/// <param name="view"></param>
		/// <param name="animate"></param>
		/// <returns></returns>
		public async Task DisplayViewAsync(ContentView host, View view, AnimateAppearance? animate = null)
		{
			if (host == null) throw new ArgumentNullException(nameof(host));
			if (view == null) throw new ArgumentNullException(nameof(view));

			switch (animate)
			{
				case AnimateAppearance.FromLeft:
					await SlideToRight(host.Content);
					break;
				case AnimateAppearance.FromRight:
					await SlideToLeft(host.Content);
					break;
			}

			host.Content = view;

			switch (animate)
			{
				case AnimateAppearance.FromLeft:
					await SlideFromLeft(host.Content);
					break;
				case AnimateAppearance.FromRight:
					await SlideFromRight(host.Content);
					break;
			}
		}

		/// <summary>
		/// Close the modal page
		/// </summary>
		/// <returns></returns>
		public Task CloseModalAsync()
		{
			return Application.Current.MainPage.Navigation.PopModalAsync();
		}

		/// <summary>
		/// Close the pop up
		/// </summary>
		/// <returns></returns>
		public Task ClosePopUpAsync()
		{
			var page = GetCurrentContentPage();

			var grid = page.Content as Grid;

			// Remove PopUp ContentView
			grid.Children.RemoveAt(grid.Children.Count - 1);

			// Remove Solid(Dim) Background
			grid.Children.RemoveAt(grid.Children.Count - 1);

			return Task.CompletedTask;
		}

		private static ContentPage GetCurrentContentPage()
		{
			var mainPage = Application.Current.MainPage;

			var modalStack = mainPage.Navigation.ModalStack;
			if (modalStack.Count > 0)
			{
				mainPage = modalStack[modalStack.Count - 1];
			}

			return mainPage as ContentPage;
		}

		private static async Task SlideFromRight(View view)
		{
			if (view == null) throw new ArgumentNullException(nameof(view));

			view.TranslationX = Application.Current.MainPage.Width * 2;

			await view.TranslateTo(0, 0);
		}

		private static async Task SlideFromLeft(View view)
		{
			if (view == null) throw new ArgumentNullException(nameof(view));

			view.TranslationX = -Application.Current.MainPage.Width;

			await view.TranslateTo(0, 0);
		}

		private static async Task SlideToLeft(View view)
		{
			if (view != null)
			{
				await view.TranslateTo(-Application.Current.MainPage.Width, 0, 400);
			}
		}

		private static async Task SlideToRight(View view)
		{
			if (view != null)
			{
				await view.TranslateTo(Application.Current.MainPage.Width * 2, 0, 400);
			}
		}
	}
}