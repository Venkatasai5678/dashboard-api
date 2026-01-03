using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT required
    public class CustomerEditController : ControllerBase
    {
        private readonly ICustomerEditService _service;

        public CustomerEditController(ICustomerEditService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllCustomers());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin role
        public IActionResult GetById(int id) => Ok(_service.GetCustomerById(id));
    }
}
