using System;

namespace AppClient.iFSA.BLL.Equipments
{
	// Complete
	public sealed class EquipmentCaptions
	{
		public string SerialNumber { get; }
		public string Power { get; }
		public string LastChecked { get; }

		public EquipmentCaptions(string serialNumber, string power, string lastChecked)
		{
			if (serialNumber == null) throw new ArgumentNullException(nameof(serialNumber));
			if (power == null) throw new ArgumentNullException(nameof(power));
			if (lastChecked == null) throw new ArgumentNullException(nameof(lastChecked));

			this.SerialNumber = serialNumber;
			this.Power = power;
			this.LastChecked = lastChecked;
		}
	}
}