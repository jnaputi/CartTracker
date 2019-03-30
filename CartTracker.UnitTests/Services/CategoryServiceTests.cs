using System;
using System.Collections.Generic;
using CartTracker.Constants;
using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Results;
using CartTracker.Services;
using CartTracker.UnitTests.Builders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CartTracker.UnitTests.Services
{
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private IService<Category> _service;

        [Fact]
        public async void TestShouldReturnAllCategoriesSuccessfully()
        {
            // Setup
            ICollection<Category> categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            _mockRepository = new Mock<IRepository<Category>>();
            _mockRepository.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(categories);

            IRepository<Category> repository = _mockRepository.Object;
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.GetAllAsync();
            
            
            // Assert
            result.Should()
                .BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.ResponseData.Should().BeAssignableTo<ICollection<Category>>()
                .Which.Count.Should().Be(3);
        }

        [Fact]
        public async void ShouldReturnAnEmptyMessageWhenTheOperationIsSuccessful()
        {
            // Setup
            ICollection<Category> categories = new List<Category>
            {
                Mock.Of<Category>(),
                Mock.Of<Category>(),
                Mock.Of<Category>()
            };

            _mockRepository = new Mock<IRepository<Category>>();
            _mockRepository.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(categories);

            IRepository<Category> repository = _mockRepository.Object;
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.GetAllAsync();
            
            
            // Assert
            result.Should()
                .BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.Message.Should().BeEmpty();
        }

        [Fact]
        public async void ShouldReturnAnEmptyCollectionWhenThereIsNoData()
        {
            // Setup
            ICollection<Category> categories = Mock.Of<ICollection<Category>>(mock => mock.Count == 0);

            _mockRepository = new Mock<IRepository<Category>>();
            _mockRepository.Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(categories);

            IRepository<Category> repository = _mockRepository.Object;
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.GetAllAsync();
            
            
            // Assert
            result.Should()
                .BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.ResponseData.Should().BeAssignableTo<ICollection<Category>>()
                .Which.Count.Should().Be(0);
        }

        [Fact]
        public async void ShouldReturnTheAppropriateErrorMessageWhenASqlExceptionIsThrown()
        {
            // Setup
            var mockSqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();

            _mockRepository = new Mock<IRepository<Category>>();
            _mockRepository.Setup(mock => mock.GetAllAsync())
                .ThrowsAsync(mockSqlException);

            IRepository<Category> repository = _mockRepository.Object;
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.GetAllAsync();
            
            
            // Assert
            result.Should()
                .BeOfType<Result<QueryResultData<ICollection<Category>>>>()
                .Which.Data.Message.Should().Be(DatabaseErrorMessages.DatabaseOperationError);
        }

        [Fact]
        public async void TestShouldReturnAFalseSuccessFlagWhenAddingANullCategory()
        {
            // Setup
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(null);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void TestShouldReturnTheCorrectErrorMessageWhenANullCategoryIsBeingAdded()
        {
            // Setup
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(null);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(CategoryErrorMessages.NullCategory);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessFlagWhenTheCategoryNameIsNull()
        {
            // Setup
            var category = new Category
            {
                Name = string.Empty
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryNameIsEmpty()
        {
            // Setup
            var category = new Category
            {
                Name = string.Empty
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(CategoryErrorMessages.NameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenTheCategoryNameIsNull()
        {
            // Setup
            var category = new Category
            {
                Name = null
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryNameIsNull()
        {
            // Setup
            var category = new Category
            {
                Name = null
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(CategoryErrorMessages.NameIsEmpty);
        }

        [Fact]
        public async void TestShouldReturnAFalseSuccessStatusWhenTheCategoryNameIsPureEmptySpaces()
        {
            // Setup
            var category = new Category
            {
                Name = "           \t\t\t\t     \t\t\t     \t\t"
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void TestShouldReturnTheCorrectMessageWhenTheCategoryNameIsPureEmptySpaces()
        {
            // Setup
            var category = new Category
            {
                Name = "           \t\t\t\t     \t\t\t     \t\t"
            };
            
            IRepository<Category> repository = Mock.Of<IRepository<Category>>();
            _service = new CategoryService(repository, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(CategoryErrorMessages.NameIsEmpty);
        }

        [Fact]
        public async void ShouldReturnAFailedSuccessStatusWhenTheCategoryAlreadyExists()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ReturnsAsync(true);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenTheCategoryAlreadyExists()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ReturnsAsync(true);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(CategoryErrorMessages.AlreadyExists);
        }

        [Fact]
        public async void ShouldAddAValidCategorySuccessfully()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ReturnsAsync(false);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            await _service.AddAsync(category);
            
            
            // Assert
            mockRepository.Verify(mock => mock.AddAsync(category), Times.Once);
        }

        [Fact]
        public async void ShouldReturnASuccessStatusWhenAValidCategoryIsAdded()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ReturnsAsync(false);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeTrue();
        }

        [Fact]
        public async void ShouldReturnAnEmptyMessageWhenAValidCategoryIsAdded()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ReturnsAsync(false);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().BeEmpty();
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenAnInvalidOperationExceptionIsThrown()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(Mock.Of<InvalidOperationException>());
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectMessageWhenAnInvalidOperationExceptionIsThrown()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(Mock.Of<InvalidOperationException>());
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(DatabaseErrorMessages.DatabaseOperationError);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenADbUpdateExceptionIsThrown()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var dbUpdateException = new DbUpdateException(string.Empty, innerException: null);

            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(dbUpdateException);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenADbUpdateExceptionIsThrown()
        {
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var dbUpdateException = new DbUpdateException(string.Empty, innerException: null);
            
            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(dbUpdateException);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(DatabaseErrorMessages.DatabaseOperationError);
        }

        [Fact]
        public async void ShouldReturnAFalseSuccessStatusWhenASqlExceptionIsThrown()
        {
            
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var sqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();
            
            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(sqlException);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Successful.Should().BeFalse();
        }
        
        [Fact]
        public async void ShouldReturnTheCorrectErrorMessageWhenASqlExceptionIsThrown()
        {
            
            // Setup
            var category = new Category
            {
                Name = "Test"
            };

            var sqlException = new SqlExceptionBuilder()
                .WithErrorNumber(1)
                .WithErrorMessage(string.Empty)
                .Builder();
            
            var mockRepository = new Mock<IRepository<Category>>();
            mockRepository.Setup(mock => mock.DataExistsAsync(category))
                .ThrowsAsync(sqlException);
            
            _service = new CategoryService(mockRepository.Object, Mock.Of<ILogger<CategoryService>>());
            
            
            // Act
            var result = await _service.AddAsync(category);
            
            
            // Assert
            result.Should().BeOfType<Result<string>>()
                .Which.Data.Should().Be(DatabaseErrorMessages.DatabaseOperationError);
        }
    }
}