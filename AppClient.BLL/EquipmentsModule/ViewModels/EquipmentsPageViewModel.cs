using System;
using AppClient.BLL.EquipmentsModule.Models;
using AppClient.BLL.Navigation;
using AppClient.Core.Core;
using AppClient.Core.ViewModels;

namespace AppClient.BLL.EquipmentsModule.ViewModels
{
	public sealed class EquipmentsPageViewModel : PageViewModel
	{
		public EquipmentsViewModel ViewModel { get; }

		public EquipmentsPageViewModel(MainContext mainContext) : base(mainContext)
		{
			if (mainContext == null) throw new ArgumentNullException(nameof(mainContext));

			// Register = OK
				//this.MainContext.RegisterServiceCreator(() => new EquipmentManager(this.MainContext));
			//this.MainContext.RegisterService(new AppNavigationService(this.MainContext));
			//var tmp = this.MainContext.GetService<AppNavigationService>();
			//tmp.OpenModalAsync(string.Empty);

			this.ViewModel = new EquipmentsViewModel(mainContext);
		}

		public override void LoadData(object parameter)
		{
			try
			{
				this.ViewModel.LoadData(this.MainContext.GetService<EquipmentManager>());
			}
			catch (Exception ex)
			{
				this.MainContext.Log(ex);
			}
		}
	}
}