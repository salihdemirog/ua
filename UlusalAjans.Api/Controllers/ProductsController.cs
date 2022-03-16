using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetById(id);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var addedProduct = _productService.Insert(product);

            return Created("", addedProduct);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var isExist = _productService.IsExist(id);

            if (!isExist)
                return NotFound();

            product.Id = id;
            _productService.Update(product);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return Ok();
        }
    }
}
