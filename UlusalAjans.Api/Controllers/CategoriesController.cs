using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Data.EntityFramework;

namespace UlusalAjans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //dispose işlemleri için using

            var categories = _categoryService.GetAll();
            return Ok(categories);

            // return NoContent();
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            //dispose işlemleri için using

            var categories = _categoryService.GetById(id);
            return Ok(categories);

            // return NoContent();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            var addedCategory = _categoryService.Insert(category);
            return Created("", addedCategory);
        }
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            var isExist = _categoryService.IsExist(id);
            if (!isExist)
                return NotFound();

            category.Id = id;
            _categoryService.Update(category);
            return Ok();

        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}