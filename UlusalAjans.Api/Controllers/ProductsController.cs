using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Data.EntityFramework;

namespace UlusalAjans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductDto product)
        {
            var addedProduct = await _productService.InsertAsync(product);

            return Created("", addedProduct);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ProductDto product)
        {
            var isExist = await _productService.IsExistAsync(id);

           
            if (!isExist)
                return NotFound();

            product.Id = id;
            await _productService.UpdateAsync(product);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);

            return Ok();
        }
    }
}
