using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalAPI.Model
{
	public class Rental
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public int CustomerID { get; set; }

		public Customer Customer { get; set; }

		[Required]
		public int BikeID { get; set; }

		public Bike Bike { get; set; }

		[Required]
		public DateTime RentalBegin { get; set; }

		public DateTime RentalEnd { get; set; }

		[Range(0, double.MaxValue)]
		[RegularExpression(@"\d+(.\d{1,2})?", ErrorMessage = "Invalid price")]
		public double TotalCost { get; set; }

		public bool Paid { get; set; }

		public bool Ended { get; set; }
	}
}
