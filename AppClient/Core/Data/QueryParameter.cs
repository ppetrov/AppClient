using System;

namespace AppClient.Core.Data
{
	/// <summary>
	/// Defines a parameter for SQL query
	/// </summary>
	public sealed class QueryParameter
	{
		public string Name { get; }
		public object Value { get; set; }

		public QueryParameter(string name, object value)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Value = value;
		}
	}
}