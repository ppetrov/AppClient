using System;

namespace AppClient.Core.Features
{
	/// <summary>
	/// Collection of all the services registered in the application
	/// </summary>
	public sealed class FeatureManager
	{
		/// <summary>
		/// The feature will be saved only if this is set to true.
		/// This can be used to temporary disable the manager
		/// </summary>
		public bool IsEnabled { get; set; } = true;

		/// <summary>
		/// Save the feature
		/// </summary>
		/// <param name="adapter"></param>
		/// <param name="feature"></param>
		public void Save(IFeatureDataAdapter adapter, Feature feature)
		{
			if (adapter == null) throw new ArgumentNullException(nameof(adapter));
			if (feature == null) throw new ArgumentNullException(nameof(feature));

			if (!this.IsEnabled) return;

			Insert(adapter, feature, string.Empty);
		}

		/// <summary>
		/// Save the feature with the associated exception
		/// </summary>
		/// <param name="adapter"></param>
		/// <param name="feature"></param>
		/// <param name="exception"></param>
		public void Save(IFeatureDataAdapter adapter, Feature feature, Exception exception)
		{
			if (adapter == null) throw new ArgumentNullException(nameof(adapter));
			if (feature == null) throw new ArgumentNullException(nameof(feature));
			if (exception == null) throw new ArgumentNullException(nameof(exception));

			if (!this.IsEnabled) return;

			Insert(adapter, feature, exception.ToString());
		}

		private static void Insert(IFeatureDataAdapter adapter, Feature feature, string details)
		{
			adapter.Save(feature, details);
		}
	}
}