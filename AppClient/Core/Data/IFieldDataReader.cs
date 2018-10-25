using System;

namespace AppClient.Core.Data
{
	/// <summary>
	/// Defines the interface for reading values from the database
	/// </summary>
	public interface IFieldDataReader
	{
		bool Read();
		bool IsDbNull(int index);
		int GetInt32(int index);
		long GetInt64(int index);
		decimal GetDecimal(int index);
		string GetString(int index);
		DateTime GetDateTime(int index);
		byte[] GetByteArray(int index);
	}
}