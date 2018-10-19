using System;
using System.Collections.Generic;

namespace AppClient.Core.Services
{
	/// <summary>
	/// Collection of all the services registered in the application
	/// </summary>
	public sealed class ServiceLocator
	{
		private Dictionary<string, object> Services { get; } = new Dictionary<string, object>();
		private HashSet<string> Creators { get; } = new HashSet<string>();

		/// <summary>
		/// Register an instance that will be re-used
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="service"></param>
		public void Register<T>(T service) where T : class
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			var key = typeof(T).FullName;

			this.Services.Add(key, service);
		}

		/// <summary>
		/// Register a function that will create a fresh instance every time
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceCreator"></param>
		public void RegisterCreator<T>(Func<T> serviceCreator) where T : class
		{
			if (serviceCreator == null) throw new ArgumentNullException(nameof(serviceCreator));

			var key = typeof(T).FullName;

			this.Services.Add(key, serviceCreator);
			this.Creators.Add(key);
		}

		/// <summary>
		/// Get an instance of the service (cached or fresh) depending on the way the service is registered
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetService<T>() where T : class
		{
			var key = typeof(T).FullName;

			var value = this.Services[key];
			if (this.Creators.Contains(key))
			{
				return ((Func<T>)value)();
			}
			return (T)value;
		}
	}
}