using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppClient.Core.Cache;
using AppClient.Core.Data;
using AppClient.Core.Dialog;
using AppClient.Core.Features;
using AppClient.Core.Localization;
using AppClient.Core.Logs;
using AppClient.Core.Services;

namespace AppClient.Core.Core
{
	/// <summary>
	/// Collection of all the services needed for the application - resolve services, feature tracking, caching, localization
	/// </summary>
	public sealed class MainContext
	{
		private ServiceLocator ServiceLocator { get; } = new ServiceLocator();
		private FeatureManager FeatureManager { get; } = new FeatureManager();
		private DataCache DataCache { get; } = new DataCache();
		private LocalizationManager LocalizationManager { get; } = new LocalizationManager();

		/// <summary>
		/// Helper method to get the registered service
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetService<T>() where T : class
		{
			return this.ServiceLocator.GetService<T>();
		}

		/// <summary>
		/// Helper method to log the exception
		/// </summary>
		/// <param name="exception"></param>
		public void Log(Exception exception)
		{
			if (exception == null) throw new ArgumentNullException(nameof(exception));

			this.GetService<Action<string, LogLevel>>()(exception.ToString(), LogLevel.Error);
		}

		/// <summary>
		/// Helper method to register a service
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="service"></param>
		public void RegisterService<T>(T service) where T : class
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			this.ServiceLocator.Register(service);
		}

		/// <summary>
		/// Helper method to register a service creator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceCreator"></param>
		public void RegisterServiceCreator<T>(Func<T> serviceCreator) where T : class
		{
			if (serviceCreator == null) throw new ArgumentNullException(nameof(serviceCreator));

			this.ServiceLocator.RegisterCreator(serviceCreator);
		}

		/// <summary>
		/// Helper method to save the feature
		/// </summary>
		/// <param name="feature"></param>
		public void Save(Feature feature)
		{
			if (feature == null) throw new ArgumentNullException(nameof(feature));

			this.FeatureManager.Save(this.GetService<IFeatureDataAdapter>(), feature);
		}

		/// <summary>
		/// Helper method to save the feature and it's exception
		/// </summary>
		/// <param name="feature"></param>
		/// <param name="exception"></param>
		public void Save(Feature feature, Exception exception)
		{
			if (feature == null) throw new ArgumentNullException(nameof(feature));
			if (exception == null) throw new ArgumentNullException(nameof(exception));

			this.FeatureManager.Save(this.GetService<IFeatureDataAdapter>(), feature, exception);
		}

		/// <summary>
		/// Helper method to get the data from the cache or load it from the data provider
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <returns></returns>
		public ReadOnlyDictionary<long, T> GetData<T>(IDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

			return this.DataCache.GetData<T>(dbContext);
		}

		/// <summary>
		/// Helper method to get the localized contents for the given key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetLocal(string key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			return this.LocalizationManager.GetLocalMessage(key).Contents;
		}

		/// <summary>
		/// Display a localized message
		/// </summary>
		/// <param name="localizationKey"></param>
		/// <returns></returns>
		public Task DisplayAsync(string localizationKey)
		{
			if (localizationKey == null) throw new ArgumentNullException(nameof(localizationKey));

			return this.GetService<IDialogManager>().DisplayAsync(this.LocalizationManager.GetLocalMessage(localizationKey));
		}

		/// <summary>
		/// Confirm a localized message
		/// </summary>
		/// <param name="localizationKey"></param>
		/// <param name="confirmationType"></param>
		/// <returns></returns>
		public Task<ConfirmationResult> ConfirmAsync(string localizationKey, ConfirmationType confirmationType)
		{
			if (localizationKey == null) throw new ArgumentNullException(nameof(localizationKey));

			return this.GetService<IDialogManager>().ConfirmAsync(this.LocalizationManager.GetLocalMessage(localizationKey), confirmationType);
		}
	}
}