using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.MODEL;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new[]
            {
                new {
                id = 1,
            name = "Mango Pickle",  
            price = 150,
            imageUrl = "/pickles/mango.jpg"

                },

                new {
                    id = 2,
                    name = "Lemon Pickle",
                    price = 120,
                    imageUrl = "/pickles/lemon.jpg"
                },
                new {
                    id = 3,
                    name = "Mixed Veg Pickle",
                    price = 180,
                    imageUrl = "/pickles/mixedveg.jpg"
                }
            };

            return Ok(products);
        }
    }
}