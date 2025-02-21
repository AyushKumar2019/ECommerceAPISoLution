using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using ECommerceAPI.RequestHelpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenricRepository<Product> repo) :BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductsSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            var page =  await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
            return page; 

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetIdByAsync(id);
            if (product == null) return NotFound();

            return product;

        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new {id=product.Id},product);
            }
            return Ok(product);
            
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id)) {
                return BadRequest("Cannot update this product");
            }  
            repo.Update(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            

            return BadRequest("Problem updating the product");
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        { 
            var product = await repo.GetIdByAsync(id);
            if (product == null) { return NotFound(); }
            repo.Remove(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem Deleting the product"); ;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        { 
            var spec = new BrandListSpecifications();

            return Ok(await repo.ListAsync(spec));
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecifications();

            return Ok(await repo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id); 
        }
    }
}
