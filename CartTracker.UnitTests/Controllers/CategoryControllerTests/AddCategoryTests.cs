using CartTracker.Constants;
using CartTracker.Controllers;
using CartTracker.Models;
using CartTracker.Results;
using CartTracker.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CartTracker.UnitTests.Controllers.CategoryControllerTests
{
    public class AddCategoryTests
    {
        private CategoryController _categoryController;
        
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