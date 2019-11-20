using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalAPI;
using BikeRentalAPI.Model;

namespace BikeRentalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RentalsController : ControllerBase
	{
		private readonly BikeRentalContext _context;
		CostCalculation _calc = new CostCalculation();

		public RentalsController(BikeRentalContext context)
		{
			_context = context;
		}

		// GET: api/Rentals
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
		{
			return await _context.Rentals.Include(r => r.Bike).Include(r => r.Customer).ToListAsync();
		}

		// GET: api/Rentals/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Rental>> GetRental(int id)
		{
			var rental = await _context.Rentals.FindAsync(id);

			if (rental == null)
			{
				return NotFound();
			}

			return rental;
		}

		// PUT: api/Rentals/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutRental(int id, Rental rental)
		{
			if (id != rental.ID)
			{
				return BadRequest();
			}

			_context.Entry(rental).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RentalExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}


		//start rental
		[Route("/start")]
		[HttpPost]
		public async Task<ActionResult<Rental>> StartRental(Rental rental)
		{

			var customer = await _context.Customers.FindAsync(rental.CustomerID);
			var bike = await _context.Bikes.FindAsync(rental.BikeID);

			customer.HasActiveRental = true;
			bike.IsRented = true;

			rental.RentalBegin = System.DateTime.Now;

			_context.Rentals.Add(rental);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetRental", new { id = rental.ID }, rental);
		}


		//end rental
		[HttpPost("{id}/end")]
		public async Task<ActionResult<Rental>> EndRental(int id)
		{
			var rental = await _context.Rentals.FindAsync(id);

			if (rental.Ended)
			{		
				return BadRequest("Rental has ended.");
			}
			if (rental == null)
			{
				return NotFound();
			}

			rental.RentalEnd = System.DateTime.Now;
			rental.Ended = true;		
			rental.Customer.HasActiveRental = false;
			rental.Bike.IsRented = false;

			
			rental.TotalCost = _calc.CalculateCost(rental.RentalBegin, rental.RentalEnd, rental.Bike.RentalPriceFirstHour, rental.Bike.RentalPricePerAdditionalHour);


			_context.Entry(rental).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();

		}

		//pay for the rental
		[HttpPost("{id}/paid")]
		public async Task<ActionResult<Rental>> PayRental(int id)
		{
			var rental = await _context.Rentals.FindAsync(id);

			if (rental == null)
			{
				return NotFound();
			}

			if (rental.TotalCost <= 0 || !rental.Ended)
			{
				return BadRequest();
			}

			rental.Paid = true;

			_context.Entry(rental).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Rentals/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Rental>> DeleteRental(int id)
		{
			var rental = await _context.Rentals.FindAsync(id);
			if (rental == null)
			{
				return NotFound();
			}

			_context.Rentals.Remove(rental);
			await _context.SaveChangesAsync();

			return rental;
		}

		private bool RentalExists(int id)
		{
			return _context.Rentals.Any(e => e.ID == id);
		}
	}
}
