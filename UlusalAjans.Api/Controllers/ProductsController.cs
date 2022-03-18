using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Data.EntityFramework;

namespace UlusalAjans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = User;
            //if(!User.Identity.IsAuthenticated)
            //    return Unauthorized();

            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogWarning("Bu bir log kaydıdır");
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
        [Authorize(Policy = "gmail")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //if(User.FindFirst(ClaimTypes.Email).Value.Contains("@gmail.com"))
            //    return Forbid();

            await _productService.DeleteAsync(id);

            return Ok();
        }
    }
}
