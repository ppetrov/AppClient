using System;
using System.Linq;

namespace AppClient.Core.Data
{
	/// <summary>
	/// Defines a wrapper for SQL query for the "T" object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Query<T>
	{
		public string Statement { get; }
		public Func<IFieldDataReader, T> Creator { get; }
		public QueryParameter[] Parameters { get; }

		public Query(string statement, Func<IFieldDataReader, T> creator, QueryParameter[] parameters = null)
		{
			if (statement == null) throw new ArgumentNullException(nameof(statement));
			if (creator == null) throw new ArgumentNullException(nameof(creator));

			this.Statement = statement;
			this.Creator = creator;
			this.Parameters = parameters ?? Enumerable.Empty<QueryParameter>().ToArray();
		}
	}

	/// <summary>
	/// Defines a wrapper for SQL query
	/// </summary>
	public sealed class Query
	{
		public string Statement { get; }
		public QueryParameter[] Parameters { get; }

		public Query(string statement, QueryParameter[] parameters = null)
		{
			if (statement == null) throw new ArgumentNullException(nameof(statement));

			this.Statement = statement;
			this.Parameters = parameters ?? Enumerable.Empty<QueryParameter>().ToArray();
		}

		public static int GetInt(IFieldDataReader r, int index, int defaultValue = 0)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? defaultValue : r.GetInt32(index);
		}

		public static long GetLong(IFieldDataReader r, int index, long defaultValue = 0)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? defaultValue : r.GetInt64(index);
		}

		public static decimal GetDecimal(IFieldDataReader r, int index, decimal defaultValue = 0M)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? defaultValue : r.GetDecimal(index);
		}

		public static string GetString(IFieldDataReader r, int index, string defaultValue = "")
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? defaultValue : r.GetString(index);
		}

		public static byte[] GetByteArray(IFieldDataReader r, int index)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? null : r.GetByteArray(index);
		}

		public static DateTime? GetDateTime(IFieldDataReader r, int index)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			return r.IsDbNull(index) ? default(DateTime?) : r.GetDateTime(index);
		}

		public static string GetGuid(IFieldDataReader r, int index)
		{
			if (r == null) throw new ArgumentNullException(nameof(r));

			if (!r.IsDbNull(index))
			{
				var value = r.GetString(index);
				var start = value.IndexOf('\'');
				if (start >= 0)
				{
					var end = value.LastIndexOf('\'');
					if (end >= 0)
					{
						start++;
						return value.Substring(start, end - start);
					}
				}
			}

			return string.Empty;
		}
	}
}