using InventoryManager.BusinessModels;
using InventoryManager.DataAccess;
using InventoryManager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        IProductHandler handler;
        public InventoryController(IProductHandler _productHandler)
        {
            handler = _productHandler;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<ActionResult<List<ProductMaster>>> Get()
        {
            return await this.handler.GetProductListAsync();
        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMaster>> Get(int id)
        {
            return await this.handler.GetProductByIdAsync(id);
        }

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<ActionResult<ProductMaster>> Post([FromBody] ProductMaster newProduct)
        {
            var itemId = await this.handler.AddProduct(newProduct);
            return await this.Get(itemId);
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this.handler.DeleteProduct(id);

                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        // PUT: api/<InventoryController>/5
        public async Task<IActionResult> Put(int id, ProductMaster product)
        {
            try
            {
                var result = await this.handler.UpdateProduct(id, product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to Update");
            }
        }
    }
}
