using System;

namespace AppClient.Core.Sort
{
	/// <summary>
	/// Defines an option to sort a collection of items by given property
	/// </summary>
	public sealed class SortOption
	{
		public string Name { get; }
		public object Property { get; }
		public SortDirection? SortDirection { get; set; }

		public SortOption(string name, object property, SortDirection? sortDirection = null)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Property = property;
			this.SortDirection = sortDirection;
		}
	}
}