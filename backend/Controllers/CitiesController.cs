using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.MODEL;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {

            _context = context;
        } 

        [HttpGet("Citiesload")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities.ToListAsync();
            return Ok(cities);
        }

        [HttpGet("CustomerLoad")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerDetails()
        {
            var customers = await _context.CustomerDetails.ToListAsync();
            return Ok(customers);
        }



        [HttpGet("Edit/{selectedId}")]
        public async Task<ActionResult<Customer>> EditCustomer(int selectedId)
        {
            var customer = await _context.CustomerDetails.FindAsync(selectedId);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }




        [HttpPost("Save")]

        public async Task<IActionResult> SaveCustomerDetails([FromBody] Customer custoerdetails)
        {
            if (custoerdetails == null)
            {
                return BadRequest("Invalid Customer data");
            }

            try
            {
                _context.CustomerDetails.Add(custoerdetails);
                await _context.SaveChangesAsync();
                return Ok("Customer saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }  
        }
    }
}
