using CartTracker.Models;
using CartTracker.Results;
using CartTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CartTracker.Constants;

namespace CartTracker.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category> _categoryService;

        public CategoryController(IService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            IResult<QueryResultData<ICollection<Category>>> queryResult = await _categoryService.GetAllAsync();

            return new ObjectResult(queryResult);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddNewCategory([FromForm] Category category)
        {
            IResult<string> insertionResult;
            
            if (category == null)
            {
                insertionResult = new Result<string>(false, CategoryErrorMessages.NullCategory);
                return new ObjectResult(insertionResult);
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                insertionResult = new Result<string>(false, CategoryErrorMessages.NameIsEmpty);
                return new ObjectResult(insertionResult);
            }

            category.Name = category.Name.Trim();

            insertionResult = await _categoryService.AddAsync(category);

            return new ObjectResult(insertionResult);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCategory([FromForm] Category category)
        {
            IResult<string> updateResult;
            
            if (category == null)
            {
                updateResult = new Result<string>(false, UpdateErrorMessages.CategoryIsNull);
                return new ObjectResult(updateResult);
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                updateResult = new Result<string>(false, CategoryErrorMessages.NameIsEmpty);

                return new ObjectResult(updateResult);
            }

            updateResult = await _categoryService.UpdateAsync(category);

            return new ObjectResult(updateResult);
        }
    }
}
