using System;

namespace AppClient.BLL.EquipmentsModule.Models
{
	// Complete
	public sealed class Equipment
	{
		public long Id { get; }
		public string SerialNumber { get; }
		public decimal Power { get; }
		public DateTime? LastChecked { get; }

		public Equipment(long id, string serialNumber, decimal power, DateTime? lastChecked)
		{
			if (serialNumber == null) throw new ArgumentNullException(nameof(serialNumber));

			this.Id = id;
			this.SerialNumber = serialNumber;
			this.Power = power;
			this.LastChecked = lastChecked;
		}
	}
}