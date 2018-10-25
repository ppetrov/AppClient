using System;
using System.Collections.Generic;

namespace AppClient.Core.Localization
{
	/// <summary>
	/// Collection of all the localized messages in the application
	/// </summary>
	public sealed class LocalizationManager
	{
		private Dictionary<string, LocalMessage> Values { get; } = new Dictionary<string, LocalMessage>();

		/// <summary>
		/// Gets the localized contents for the given key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public LocalMessage GetLocalMessage(string key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			this.Values.TryGetValue(key, out var value);

			return value ?? LocalMessage.Empty;
		}

		/// <summary>
		/// Load the localized contents. If the collection contains duplicate keys the last one will be used.
		/// </summary>
		/// <param name="values"></param>
		public void Load(IEnumerable<KeyValuePair<string, string>> values)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));

			this.Values.Clear();
			foreach (var kv in values)
			{
				this.Values[kv.Key] = new LocalMessage(kv.Value);
			}
		}
	}
}