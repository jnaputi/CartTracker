using CartTracker.Controllers;
using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using CartTracker.Results;
using CartTracker.UnitTests.Builders;
using Xunit;

namespace CartTracker.UnitTests.Controllers
{
    public class CategoryControllerTests
    {
        private IRepository<Category> _categoryRepository;
        private CategoryService _categoryService;
        private CategoryController _categoryController;

        [Fact]
        public async void ShouldGetAllCategoriesSuccessfully()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var mockCategoryRepository = new Mock<IRepository<Category>>();
            mockCategoryRepository.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(categories);
            _categoryRepository = mockCategoryRepository.Object;

            _categoryService = new CategoryService(_categoryRepository);
            _categoryController = new CategoryController(_categoryService);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<QueryResult<ICollection<Category>>>()
                .Which.Data.Count.Should().Be(3);
        }

        [Fact]
        public async void ShouldHaveANullErrorMessageWhenQueryIsSuccessful()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var mockCategoryRepository = new Mock<IRepository<Category>>();
            mockCategoryRepository.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(categories);
            _categoryRepository = mockCategoryRepository.Object;

            _categoryService = new CategoryService(_categoryRepository);
            _categoryController = new CategoryController(_categoryService);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<QueryResult<ICollection<Category>>>()
                .Which.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async void ShouldReturnANullCollectionWhenASqlExceptionIsThrown()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var mockSqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();

            var mockCategoryRepository = new Mock<IRepository<Category>>();
            mockCategoryRepository.Setup(mock => mock.GetAllAsync())
                .ThrowsAsync(mockSqlException);
            _categoryRepository = mockCategoryRepository.Object;

            _categoryService = new CategoryService(_categoryRepository);
            _categoryController = new CategoryController(_categoryService);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<QueryResult<ICollection<Category>>>()
                .Which.Data.Should().BeNull();
        }

        [Fact]
        public async void ShouldReturnAnErrorMessageWhenASqlExceptionIsThrown()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var mockSqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();

            var mockCategoryRepository = new Mock<IRepository<Category>>();
            mockCategoryRepository.Setup(mock => mock.GetAllAsync())
                .ThrowsAsync(mockSqlException);
            _categoryRepository = mockCategoryRepository.Object;

            _categoryService = new CategoryService(_categoryRepository);
            _categoryController = new CategoryController(_categoryService);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<QueryResult<ICollection<Category>>>()
                .Which.ErrorMessage.Should().NotBeNullOrEmpty();
        }
    }
}
