using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlusalAjans.Data.Abstract;

namespace UlusalAjans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _taskService.GetAllProduct();
            var categories = _taskService.GetAllCategory();

            var productTask = Task.Run(() => _taskService.GetAllProduct());
            var categoriesTask = Task.Run(() => _taskService.GetAllCategory());

            Task.WaitAll(productTask, categoriesTask);

            return Ok(new
            {
                products = products,
                categories = categories
            });
        }

        [HttpGet("async")]
        public IActionResult GetAsync()
        {
            var productTask = _taskService.GetAllProductAsync();
            var categoriesTask =  _taskService.GetAllCategoryAsync();

            // Task[] tasks= new Task[] { productTask, categoriesTask };

            //int index= Task.WaitAny(productTask, categoriesTask);

            Task.WaitAll(productTask, categoriesTask);
            
            return Ok(new
            {
                products = productTask.Result,
                categories = categoriesTask.Result
            });
        }
    }
}
