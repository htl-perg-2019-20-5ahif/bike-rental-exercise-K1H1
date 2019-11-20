using BikeRentalAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAPI
{
	public class BikeRentalContext: DbContext
	{

		public BikeRentalContext(DbContextOptions<BikeRentalContext> options)
		: base(options)
		{ }


		public DbSet<Customer> Customers { get; set; }

		public DbSet<Bike> Bikes { get; set; }

		public DbSet<Rental> Rentals { get; set; }

		/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//local SQLServer with WindowsAuthentification
			optionsBuilder.UseSqlServer("Server=KATRIN-BOOK;Database=BikeRental;Integrated Security=SSPI;");
		}*/

		
	}
}
