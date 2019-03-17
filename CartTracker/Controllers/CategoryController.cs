using CartTracker.Models;
using CartTracker.Results;
using CartTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartTracker.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            QueryResult<ICollection<Category>> queryResult = await _categoryService.GetCategoriesAsync();

            return new ObjectResult(queryResult);
        }
    }
}
