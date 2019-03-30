using System.Collections.Generic;
using CartTracker.Controllers;
using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Results;
using CartTracker.Services;
using CartTracker.UnitTests.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CartTracker.UnitTests.Controllers.CategoryControllerTests
{
    public class GetAllCategoriesTests
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
    }
}