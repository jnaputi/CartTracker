using System;
using CartTracker.Models;
using CartTracker.Results;
using CartTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CartTracker.Constants;
using Microsoft.Extensions.Logging;

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

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            IResult<QueryResultData<ICollection<Category>>> queryResult = await _categoryService.GetAllAsync();

            return new ObjectResult(queryResult);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromForm] Category category)
        {
            IResult<string> insertionResult;
            
            if (category == null)
            {
                insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryIsNull);
                return new ObjectResult(insertionResult);
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryNameIsEmpty);
                return new ObjectResult(insertionResult);
            }

            category.Name = category.Name.Trim();

            insertionResult = await _categoryService.AddAsync(category);

            return new ObjectResult(insertionResult);
        }
    }
}
