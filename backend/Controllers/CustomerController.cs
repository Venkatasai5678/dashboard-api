using backend.MODEL;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("Citiesload")]

        //public async Task<ActionResult> getcitues()
        //{

        //}
        //public async Task<ActionResult> getcitues()
        //{

        //}
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesDetails()
        {
            var customers = await _customerService.GetAllCitiesAsync();
            return Ok(customers);
        }

        
        [HttpGet("CustomerLoad")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerDetails()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("Edit/{selectedId}")]
        public async Task<ActionResult<Customer>> EditCustomer(int selectedId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(selectedId);
            if (customer == null) return NotFound();
            return Ok(customer);

        }
        [HttpPost("Save")]
        public async Task<IActionResult> SaveCustomerDetails([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest(new { success = false, message = "Invalid data" });

            await _customerService.AddCustomerAsync(customer);

            return Ok(new
            {
                success = true,
                message = "Customer saved successfully"
            });
        }
        //hjh

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (id != updatedCustomer.Id)
                return BadRequest("ID mismatch");

            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return NotFound("Customer not found");

            try
            {
                await _customerService.UpdateCustomerAsync(updatedCustomer);
                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("Savecontact")]

        public async Task<IActionResult> SaveContactDetails([FromBody] Contact Contact)
        {
            if (Contact == null) return BadRequest("bad request ");
            try
            {
                await _customerService.AddContactAsync(Contact);
                return Ok("Customer saved successfully.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "fdzs");
            }
        }


        [HttpGet("TestError")]
        public IActionResult TestError()
        {
            int a = 10;
            int b = 0;

            int result = a / b; // 💥 DivideByZeroException

            return Ok(result);
        }


    }
}