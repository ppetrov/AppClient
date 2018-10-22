using System;
using System.Threading.Tasks;
using AppClient.Core.Core;
using AppClient.Core.ViewModels;
using Xamarin.Forms;

namespace AppClient.BLL.Navigation
{
	public sealed class AppNavigationService
	{
		private MainContext MainContext { get; }

		public AppNavigationService(MainContext mainContext)
		{
			if (mainContext == null) throw new ArgumentNullException(nameof(mainContext));

			this.MainContext = mainContext;
		}

		public void SetMainPage(string viewModelName, object parameter = null)
		{
			// TODO : !!!
		}

		public Task<PageViewModel> OpenModalAsync(string viewModelName, object parameter = null)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));

			// TODO : !!!
			return null;
		}

		public Task<PageViewModel> OpenPopUpAsync(string viewModelName, object parameter = null, PopUpSettings settings = null)
		{
			if (viewModelName == null) throw new ArgumentNullException(nameof(viewModelName));

			// TODO : !!!
			return null;
		}

		public Task CloseModalAsync()
		{
			// TODO : !!!
			return null;
		}

		public Task ClosePopUpAsync()
		{
			// TODO : !!!
			return null;
		}
	}
}