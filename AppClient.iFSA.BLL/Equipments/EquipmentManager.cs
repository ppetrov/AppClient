using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppClient.Core.Core;
using AppClient.Core.Data;
using AppClient.Core.Dialog;

namespace AppClient.iFSA.BLL.Equipments
{
	public sealed class EquipmentManager
	{
		private MainContext MainContext { get; }

		public EquipmentManager(MainContext mainContext)
		{
			if (mainContext == null) throw new ArgumentNullException(nameof(mainContext));

			this.MainContext = mainContext;
		}

		public List<Equipment> GetEquipments()
		{
			List<Equipment> equipments;

			using (var dbContext = this.MainContext.GetService<IDbContext>())
			{
				equipments = GetEquipments(dbContext);

				dbContext.Complete();
			}

			return equipments;
		}

		public async Task<Equipment> AddAsync(Equipment equipment)
		{
			if (equipment == null) throw new ArgumentNullException(nameof(equipment));

			using (var dbContext = this.MainContext.GetService<IDbContext>())
			{
				// Validation & confirmation
				if (string.IsNullOrWhiteSpace(equipment.SerialNumber))
				{
					await this.MainContext.DisplayAsync(@"Missing serial number");
					return null;
				}

				var serialNumber = equipment.SerialNumber;

				//if equipments from the parameter is empty ?! it will lie to the method
				// and later it will fail by PK
				// What it I pass filtered equipments ?! instead of all (real scenario)
				// What if some background job downloaded new equipments
				// What if we don't load all the equipments ? but only for a given date
				// We still need to check ALL (database)
				var equipments = this.GetEquipments();

				if (equipments.Exists(e => e.SerialNumber == serialNumber))
				{
					await this.MainContext.DisplayAsync(@"Exists");

					return null;
				}

				// TODO : Check the parameter
				var parameters = this.MainContext.GetData<string>(dbContext);
				var warningEquipmentPower = 1000;

				if (equipment.Power > warningEquipmentPower)
				{
					var confirmation = await this.MainContext.ConfirmAsync(@"Confirm BIG power", ConfirmationType.YesNo);
					if (confirmation != ConfirmationResult.Accept)
					{
						return null;
					}
				}

				var newEquipment = Insert(dbContext, equipment);

				dbContext.Complete();

				return newEquipment;
			}
		}

		public async Task<bool> DeleteAsync(Equipment equipment)
		{
			if (equipment == null) throw new ArgumentNullException(nameof(equipment));

			using (var dbContext = this.MainContext.GetService<IDbContext>())
			{
				if (!equipment.LastChecked.HasValue)
				{
					await this.MainContext.DisplayAsync(@"N/A");
					return false;
				}

				if ((equipment.LastChecked.Value - DateTime.Today) > TimeSpan.Zero)
				{
					var dialog = this.MainContext.GetService<IDialogManager>();
					var local = this.MainContext.GetLocal(@"Confirm BIG power");
					var confirmation = await this.MainContext.ConfirmAsync(local, ConfirmationType.YesNo);
					if (confirmation != ConfirmationResult.Accept)
					{
						return false;
					}
				}

				var isDeleted = Delete(dbContext, equipment);

				dbContext.Complete();

				return isDeleted;
			}
		}

		private static List<Equipment> GetEquipments(IDbContext dbContext)
		{
			var query = new Query<Equipment>(@"SELECT Id, Serial_Number, Power, Last_Checked FROM Equipments", r =>
			{
				var id = Query.GetLong(r, 0);
				var serialNumber = Query.GetString(r, 1);
				var power = Query.GetDecimal(r, 2);
				var lastChecked = Query.GetDateTime(r, 3);

				return new Equipment(id, serialNumber, power, lastChecked);
			});

			return dbContext.Execute(query).ToList();
		}

		private static Equipment Insert(IDbContext dbContext, Equipment equipment)
		{
			throw new NotImplementedException();
		}

		private static bool Delete(IDbContext dbContext, Equipment equipment)
		{
			throw new NotImplementedException();
		}
	}
}