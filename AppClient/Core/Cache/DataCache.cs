using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AppClient.Core.Data;

namespace AppClient.Core.Cache
{
	/// <summary>
	/// Collection of all the cached data in the application
	/// </summary>
	public sealed class DataCache
	{
		private Dictionary<string, object> Data { get; } = new Dictionary<string, object>();
		private Dictionary<string, object> DataProviders { get; } = new Dictionary<string, object>();

		/// <summary>
		/// Indicates whether or not to use the cache.
		/// </summary>
		public bool IsEnabled { get; set; } = true;

		/// <summary>
		/// Register a data provider
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataProvider"></param>
		public void Register<T>(Func<IDbContext, DataCache, Dictionary<long, T>> dataProvider)
		{
			if (dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));

			var key = typeof(T).FullName;

			this.DataProviders.Add(key, dataProvider);
		}

		/// <summary>
		/// Get the data from the cache or load it from the respective data provider.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <returns></returns>
		public ReadOnlyDictionary<long, T> GetData<T>(IDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

			var key = typeof(T).FullName;

			Dictionary<long, T> data;

			if (this.IsEnabled)
			{
				if (!this.Data.TryGetValue(key, out var cachedData))
				{
					cachedData = (this.DataProviders[key] as Func<IDbContext, DataCache, Dictionary<long, T>>)(dbContext, this);
					this.Data.Add(key, cachedData);
				}
				data = (Dictionary<long, T>)cachedData;
			}
			else
			{
				data = (this.DataProviders[key] as Func<IDbContext, DataCache, Dictionary<long, T>>)(dbContext, this);
			}

			return new ReadOnlyDictionary<long, T>(data);
		}

		/// <summary>
		/// Clear the data from the cache
		/// </summary>
		public void Clear()
		{
			this.Data.Clear();
		}
	}
}