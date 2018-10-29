using System;
using System.Linq;
using System.Threading.Tasks;
using AppClient.Core.Navigation;
using Xamarin.Forms.Internals;

namespace AppClient.Core.Flows.MultiStepWizard
{
	public sealed class Wizard
	{
		private int[] Steps { get; }

		public int Current { get; private set; }
		public bool HasNext => this.Current != this.Steps.Last();
		public bool HasPrevious => this.Current != this.Steps.First();

		public Func<Task<bool>> CanGoNextAsync { get; }
		public Func<Task<int>> GoToNextStepAsync { get; }
		public Func<Task<bool>> CanGoPreviousAsync { get; }
		public Func<Task<int>> GoToPreviousStepAsync { get; }

		public Wizard(int[] steps, int current, Func<Task<bool>> canGoNextAsync, Func<Task<int>> goToNextStepAsync, Func<Task<bool>> canGoPreviousAsync, Func<Task<int>> goToPreviousStepAsync)
		{
			if (steps == null) throw new ArgumentNullException(nameof(steps));
			if (canGoNextAsync == null) throw new ArgumentNullException(nameof(canGoNextAsync));
			if (goToNextStepAsync == null) throw new ArgumentNullException(nameof(goToNextStepAsync));
			if (canGoPreviousAsync == null) throw new ArgumentNullException(nameof(canGoPreviousAsync));
			if (goToPreviousStepAsync == null) throw new ArgumentNullException(nameof(goToPreviousStepAsync));

			this.Steps = steps;
			this.Current = current;
			this.GoToPreviousStepAsync = goToPreviousStepAsync;
			this.CanGoPreviousAsync = canGoPreviousAsync;
			this.GoToNextStepAsync = goToNextStepAsync;
			this.CanGoNextAsync = canGoNextAsync;
		}

		public async Task<int> NavigateAsync(int offset, Func<AnimateAppearance?, Task> displayView, AnimateAppearance? appearance)
		{
			this.Current = this.Steps[this.Steps.IndexOf(this.Current) + offset];

			await displayView(appearance);

			return this.Current;
		}
	}
}