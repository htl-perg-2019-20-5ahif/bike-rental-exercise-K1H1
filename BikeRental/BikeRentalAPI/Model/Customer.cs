using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalAPI.Model
{
	public enum Gender { Male, Female, Unknown }
	public class Customer
	{

		[Key]
		public int ID { get; set; }

		[Required]
		public Gender Gender { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(75)]
		public string LastName { get; set; }

		[Required]
		public DateTime Birthday { get; set; }

		[Required]
		[MaxLength(75)]
		public string Street { get; set; }

		[Required]
		[MaxLength(10)]
		public string HouseNumber { get; set; }

		[Required]
		[MaxLength(10)]
		public string ZipCode { get; set; }

		[Required]
		[MaxLength(75)]
		public string Town { get; set; }

		public bool HasActiveRental { get; set; }

		public List<Rental> Rentals { get; set; }
	}
}
