using System;

namespace AppClient.Core.Localization
{
	/// <summary>
	/// Defines a localized message
	/// </summary>
	public sealed class LocalMessage
	{
		public static readonly LocalMessage Empty = new LocalMessage(string.Empty);

		public string Contents { get; }

		public LocalMessage(string contents)
		{
			if (contents == null) throw new ArgumentNullException(nameof(contents));

			this.Contents = contents;
		}
	}
}