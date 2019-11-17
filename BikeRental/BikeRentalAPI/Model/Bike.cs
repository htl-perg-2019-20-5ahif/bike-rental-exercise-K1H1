using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalAPI.Model
{
	public enum BikeCategory { Standard, Mountain, Trecking, Racing }

	public class Bike
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[MaxLength(25)]
		public string Brand { get; set; }

		[Required]
		public DateTime PurchaseDate { get; set; }

		[MaxLength(1000)]
		public string Notes { get; set; }

		public DateTime LastServiceDate { get; set; }

		[Required]
		[Range(0.0, double.MaxValue)]
		[RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "Invalid price")]
		public double RentalPriceFirstHour { get; set; }

		[Required]
		[Range(1.0, double.MaxValue)]// \d+(\.\d{1,2})?
		[RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "Invalid price")]
		public double RentalPricePerAdditionalHour { get; set; }

		[Required]
		public BikeCategory BikeCategory { get; set; }

		public bool IsRented { get; set; }

		public List<Rental> Rentals { get; set; }
	}
}
