using BikeRentalAPI;
using System;
using Xunit;

namespace BikeRentalTests
{
	public class UnitTest1
	{
		[Fact]
		public void TestCalculation()
		{
			CostCalculation _calc = new CostCalculation();
			var price = _calc.CalculateCost(new DateTime(2019, 2, 14, 8, 15, 0), new DateTime(2019, 2, 14, 10, 30, 0), 3, 5);
			Assert.Equal(13, price);

			price = _calc.CalculateCost(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 45, 0), 3, 100);
			Assert.Equal(3, price);

			price = _calc.CalculateCost(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 25, 0), 20, 100);
			Assert.Equal(0, price);

			
		}
	
	}
}
