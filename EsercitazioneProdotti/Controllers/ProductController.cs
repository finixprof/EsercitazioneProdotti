using EsercitazioneProdotti.Helpers;
using EsercitazioneProdotti.Models;
using EsercitazioneProdotti.Models.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsercitazioneProdotti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {


        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return FakeDatabaseHelper.GetAllProducts();
        }

        [HttpGet("{id}")] //se tolgo "{id}" creo ambiguità 
        public IActionResult Get(int id)
        {
            try
            {
                var product = FakeDatabaseHelper.GetProductById(id);
                return Ok(product);
            }
            catch (CustomException ex)
            {
                if (ex.StatusCode == 404)
                    return NotFound();
                else
                    return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (FakeDatabaseHelper.Save(product))
                return Ok(product.Id);
            return BadRequest();
        }


        [HttpPatch("{id}")]
        public IActionResult Put(string id, [FromBody] Product product)
        {
            if (FakeDatabaseHelper.Save(product))
            return NoContent();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (FakeDatabaseHelper.Delete(id))
                return NoContent();
            return BadRequest();
        }

    }
}
