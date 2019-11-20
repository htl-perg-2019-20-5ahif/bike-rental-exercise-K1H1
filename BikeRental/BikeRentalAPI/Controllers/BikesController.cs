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
	public class BikesController : ControllerBase
	{
		private readonly BikeRentalContext _context;

		public BikesController(BikeRentalContext context)
		{
			_context = context;
		}

		// GET: api/Bikes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Bike>>> GetBikes([FromQuery]string sortBy, bool available)
		{
			var bikes = await _context.Bikes.Include(b => b.Rentals).ToListAsync();

			if (available)
			{
				bikes = bikes.Where(b => !b.IsRented).ToList();
			}

			List<Bike> sortedList = new List<Bike>();
			switch (sortBy)
			{
				case "purchaseDate": sortedList= bikes.OrderByDescending(b => b.PurchaseDate).ToList(); break;
				case "priceFirstHour": sortedList= bikes.OrderBy(b => b.RentalPriceFirstHour).ToList(); break;
				case "priceAdditionalHour": sortedList= bikes.OrderBy(b => b.RentalPricePerAdditionalHour).ToList(); break;
				default: sortedList= bikes; break;
				
			}
			return sortedList;
		}

		// GET: api/Bikes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Bike>> GetBike(int id)
		{
			var bike = await _context.Bikes.Include(b => b.Rentals).Where(b => b.ID == id).FirstOrDefaultAsync();

			if (bike == null)
			{
				return NotFound();
			}

			return bike;
		}

		// PUT: api/Bikes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBike(int id, Bike bike)
		{
			if (id != bike.ID)
			{
				return BadRequest();
			}

			_context.Entry(bike).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BikeExists(id))
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

		// POST: api/Bikes
		[HttpPost]
		public async Task<ActionResult<Bike>> PostBike(Bike bike)
		{
			_context.Bikes.Add(bike);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetBike", new { id = bike.ID }, bike);
		}

		// DELETE: api/Bikes/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Bike>> DeleteBike(int id)
		{
			var bike = await _context.Bikes.Include(b => b.Rentals).Where(b => b.ID == id).FirstOrDefaultAsync();
			if (bike == null)
			{
				return NotFound();
			}

			_context.Bikes.Remove(bike);
			await _context.SaveChangesAsync();

			return bike;
		}

		private bool BikeExists(int id)
		{
			return _context.Bikes.Any(e => e.ID == id);
		}
	}
}
