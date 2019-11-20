using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalAPI
{

	public class CostCalculation
	{
		DateTime RentalBegin;
		DateTime RentalEnd;
		double RentalPriceFirstHour;
		double RentalPricePerAdditionalHour;


		public CostCalculation()
		{

		}

		public double CalculateCost(DateTime RentalBegin, DateTime RentalEnd, double PriceFirstHour, double PriceAdditionalHour)
		{
			double totalCosts=0;
			var rentalTime = this.RentalEnd - this.RentalBegin;

			if (rentalTime.TotalMinutes >=15)
			{
				totalCosts += this.RentalPriceFirstHour;
			}

			var additionalHours = (int)Math.Ceiling((rentalTime - TimeSpan.FromHours(1)).TotalHours);

			if(additionalHours > 0)
			{
				totalCosts += additionalHours * this.RentalPricePerAdditionalHour;
			}
			return totalCosts;
		}
	
	}
}
