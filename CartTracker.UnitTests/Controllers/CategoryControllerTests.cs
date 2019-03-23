using CartTracker.Controllers;
using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using CartTracker.Constants;
using CartTracker.Results;
using CartTracker.UnitTests.Builders;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CartTracker.UnitTests.Controllers
{
    public class CategoryControllerTests
    {
        private CategoryController _categoryController;

        [Fact]
        public async void TestShouldGetAllCategoriesSuccessfully()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var queryResultData = new QueryResultData<ICollection<Category>>(categories, string.Empty);
            var serviceResult = new Result<QueryResultData<ICollection<Category>>>(true, queryResultData);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(serviceResult);

            _categoryController = new CategoryController(mockService.Object);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.ResponseData.Count.Should().Be(3);
        }

        [Fact]
        public async void TestShouldHaveAnEmptyErrorMessageWhenQueryIsSuccessful()
        {
            // Setup
            var categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            var queryResultData = new QueryResultData<ICollection<Category>>(categories, string.Empty);
            var serviceResult = new Result<QueryResultData<ICollection<Category>>>(true, queryResultData);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(serviceResult);

            _categoryController = new CategoryController(mockService.Object);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.Message.Should().BeEmpty();
        }

        [Fact]
        public async void TestShouldReturnANullCollectionWhenASqlExceptionIsThrown()
        {
            // Setup
            var mockSqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.GetAllAsync())
                .ThrowsAsync(mockSqlException);
            
            var service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            _categoryController = new CategoryController(service);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.ResponseData.Should().BeNull();
        }

        [Fact]
        public async void TestShouldReturnAnErrorMessageWhenASqlExceptionIsThrown()
        {
            // Setup
            var mockSqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.GetAllAsync())
                .ThrowsAsync(mockSqlException);
            
            var service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            _categoryController = new CategoryController(service);


            // Act
            IActionResult result = await _categoryController.GetAllCategories();


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.Message.Should().NotBeNullOrEmpty();
        }

        #region AddNewCategory Tests

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenTheCategoryIsNull()
        {
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(Mock.Of<Category>()))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(null);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryIsNull()
        {
            var insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryIsNull);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(Mock.Of<Category>()))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(null);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryIsNull);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenTheCategoryNameIsNull()
        {
            var category = new Category
            {
                Name = null
            };
            
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }
        
        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryNameIsNull()
        {
            var category = new Category
            {
                Name = null
            };
            
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryNameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenTheCategoryNameIsEmpty()
        {
            var category = new Category
            {
                Name = string.Empty
            };
            
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryNameIsEmpty()
        {
            var category = new Category
            {
                Name = string.Empty
            };
            
            var insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryNameIsEmpty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryNameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenTheCategoryNameIsPureWhiteSpace()
        {
            var category = new Category
            {
                Name = "  \t\t\t         \t\t\t        "
            };
            
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryNameIsPureWhiteSpace()
        {
            var category = new Category
            {
                Name = "  \t\t\t         \t\t\t        "
            };
            
            var insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryNameIsEmpty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryNameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnASuccessStatusWhenTheCategoryIsValid()
        {
            var category = new Category
            {
                Name = "  \t\t\t         \t\t\t        "
            };
            
            var insertionResult = new Result<string>(false, InsertionErrorMessages.CategoryNameIsEmpty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryNameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnAFalseStatusWhenTheCategoryIsNullInService()
        {
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(null))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(null);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryIsNullInService()
        {
            var insertionResult = new Result<string>(false, string.Empty);

            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(null))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);
            

            // Act
            IActionResult result = await _categoryController.AddNewCategory(null);

            
            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(InsertionErrorMessages.CategoryIsNull);
        }

        [Fact]
        public async void ShouldReturnASuccessStatusWhenTheCategoryIsAddedSuccessfully()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var insertionResult = new Result<string>(true, string.Empty);
            
            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);


            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeTrue();
        }

        [Fact]
        public async void ShouldReturnAnEmptyStringWhenTheCategoryIsSuccessfullyAdded()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var insertionResult = new Result<string>(true, string.Empty);
            
            var mockService = new Mock<IService<Category>>();
            mockService.Setup(mock => mock.AddAsync(category))
                .ReturnsAsync(insertionResult);

            _categoryController = new CategoryController(mockService.Object);


            // Act
            IActionResult result = await _categoryController.AddNewCategory(category);


            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeOfType<Result<string>>()
                .Which.Data.Should().BeEmpty();
        }

        #endregion
    }
}
