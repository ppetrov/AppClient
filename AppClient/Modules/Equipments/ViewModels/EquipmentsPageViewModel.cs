using System;
using AppClient.Core;
using AppClient.Core.ViewModels;
using AppClient.Modules.Equipments.Models;

namespace AppClient.Modules.Equipments.ViewModels
{
	public sealed class EquipmentsPageViewModel : PageViewModel
	{
		public EquipmentsViewModel ViewModel { get; }

		public EquipmentsPageViewModel(MainContext mainContext) : base(mainContext)
		{
			if (mainContext == null) throw new ArgumentNullException(nameof(mainContext));

			this.ViewModel = new EquipmentsViewModel(mainContext);
		}

		public void LoadData()
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