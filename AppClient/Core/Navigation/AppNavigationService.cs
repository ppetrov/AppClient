using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppClient.Core.ViewModels;
using Xamarin.Forms;

namespace AppClient.Core.Navigation
{
	public sealed class AppNavigationService
	{
		private Dictionary<string, Tuple<PageViewModel, ContentPage>> PageMappings { get; } = new Dictionary<string, Tuple<PageViewModel, ContentPage>>();
		private Dictionary<string, Tuple<PageViewModel, ContentView>> ViewsMappings { get; } = new Dictionary<string, Tuple<PageViewModel, ContentView>>();

		public void Register(string viewModelName, Tuple<PageViewModel, ContentPage> mapping)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));
			if (mapping == null) throw new ArgumentNullException(nameof(mapping));

			this.PageMappings.Add(viewModelName, mapping);
		}

		public void Register(string viewModelName, Tuple<PageViewModel, ContentView> mapping)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));
			if (mapping == null) throw new ArgumentNullException(nameof(mapping));

			this.ViewsMappings.Add(viewModelName, mapping);
		}

		/// <summary>
		/// Set the main page of the application
		/// </summary>
		/// <param name="viewModelName"></param>
		/// <param name="parameter"></param>
		public void SetMainPage(string viewModelName, object parameter = null)
		{
			Application.Current.MainPage = this.CreatePage(viewModelName, parameter);
		}

		/// <summary>
		/// Open the corresponding page as a modal page
		/// </summary>
		/// <param name="viewModelName"></param>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public async Task<PageViewModel> OpenModalAsync(string viewModelName, object parameter = null)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));

			var page = this.CreatePage(viewModelName, parameter);

			await Application.Current.MainPage.Navigation.PushModalAsync(page);

			return page.BindingContext as PageViewModel;
		}

		public Task<PageViewModel> OpenPopUpAsync(string viewModelName, object parameter = null, PopUpSettings settings = null)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));

			// TODO : !!!
			return null;
		}

		/// <summary>
		/// Close the modal page
		/// </summary>
		/// <returns></returns>
		public Task CloseModalAsync()
		{
			return Application.Current.MainPage.Navigation.PopModalAsync();
		}

		public Task ClosePopUpAsync()
		{
			// TODO : !!!
			return null;
		}

		private ContentPage CreatePage(string viewModelName, object parameter)
		{
			var values = this.PageMappings[viewModelName];

			// Create ViewModel
			var viewModel = values.Item1;

			// Load ViewModel with parameter
			viewModel.LoadData(parameter);

			var page = values.Item2;

			// Set bindingContext
			page.BindingContext = viewModel;

			return page;
		}
	}
}