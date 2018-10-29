using System;
using AppClient.Core.Features;
using AppClient.Core.ViewModels;
using Xamarin.Forms;

namespace AppClient.Core.Flows.MultiStepWizard
{
	public abstract class WizardPageViewModel : PageViewModel
	{
		private Wizard Wizard { get; set; }

		private int _step;
		public int Step
		{
			get { return _step; }
			set
			{
				this.SetProperty(ref _step, value);
				this.HasNext = this.Wizard.HasNext;
				this.HasPrevious = this.Wizard.HasPrevious;
			}
		}

		private bool _hasNext;
		public bool HasNext
		{
			get { return _hasNext; }
			set { this.SetProperty(ref _hasNext, value); }
		}
		public Command NextCommand { get; }

		private bool _hasPrevious;
		public bool HasPrevious
		{
			get { return _hasPrevious; }
			set { this.SetProperty(ref _hasPrevious, value); }
		}
		public Command PreviousCommand { get; }

		protected WizardPageViewModel(MainContext mainContext) : base(mainContext)
		{
			this.NextCommand = new Command(this.Next);
			this.PreviousCommand = new Command(this.Previous);
		}

		public void Setup(Wizard wizard)
		{
			if (wizard == null) throw new ArgumentNullException(nameof(wizard));

			try
			{
				this.Wizard = wizard;
				this.Step = this.Wizard.Current;
			}
			catch (Exception ex)
			{
				this.MainContext.Log(ex);
			}
		}

		private async void Next()
		{
			var feature = new Feature(nameof(WizardPageViewModel), nameof(this.Next));
			try
			{
				this.MainContext.Save(feature);

				if (await this.Wizard.CanGoNextAsync())
				{
					this.Step = await this.Wizard.GoToNextStepAsync();
				}
			}
			catch (Exception ex)
			{
				this.MainContext.Save(feature, ex);
			}
		}

		private async void Previous()
		{
			var feature = new Feature(nameof(WizardPageViewModel), nameof(this.Previous));
			try
			{
				this.MainContext.Save(feature);

				if (await this.Wizard.CanGoPreviousAsync())
				{
					this.Step = await this.Wizard.GoToPreviousStepAsync();
				}
			}
			catch (Exception ex)
			{
				this.MainContext.Save(feature, ex);
			}
		}
	}
}