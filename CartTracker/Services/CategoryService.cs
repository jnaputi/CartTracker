using System;
using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Results;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CartTracker.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CartTracker.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IRepository<Category> categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IResult<QueryResultData<ICollection<Category>>>> GetAllAsync()
        {
            ICollection<Category> categories;
            QueryResultData<ICollection<Category>> resultData;
            
            try
            {
                categories = await _categoryRepository.GetAllAsync();
            }
            catch(SqlException sqlException)
            {
                _logger.LogError(sqlException.Message);

                resultData =
                    new QueryResultData<ICollection<Category>>(
                        null, DatabaseErrorMessages.DatabaseOperationError);

                var result = new Result<QueryResultData<ICollection<Category>>>(
                    false, resultData);

                return result;
            }

            resultData = new QueryResultData<ICollection<Category>>(categories, string.Empty);
            
            return new Result<QueryResultData<ICollection<Category>>>(
                true, resultData);
        }

        public async Task<IResult<string>> AddAsync(Category entityToAdd)
        {
            if (entityToAdd == null)
            {
                return new Result<string>(false, CategoryErrorMessages.NullCategory);
            }

            if (string.IsNullOrWhiteSpace(entityToAdd.Name))
            {
                return new Result<string>(false, CategoryErrorMessages.NameIsEmpty);
            }

            entityToAdd.Name = entityToAdd.Name.Trim();

            try
            {
                var categoryExists = await _categoryRepository.DataExistsAsync(entityToAdd);
                if (categoryExists)
                {
                    return new Result<string>(false, CategoryErrorMessages.AlreadyExists);
                }

                await _categoryRepository.AddAsync(entityToAdd);
            }
            catch (InvalidOperationException e)
            {
                _logger.Log(LogLevel.Error, e.Message);

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }
            catch (DbUpdateException e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }
            catch (SqlException e)
            {
                _logger.Log(LogLevel.Error, e.Message);

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }

            return new Result<string>(true, string.Empty);
        }

        public async Task<IResult<string>> UpdateAsync(Category updatedEntity)
        {
            if (updatedEntity == null)
            {
                return new Result<string>(false, CategoryErrorMessages.NullCategory);
            }

            if (string.IsNullOrWhiteSpace(updatedEntity.Name))
            {
                return new Result<string>(false, CategoryErrorMessages.NameIsEmpty);
            }

            try
            {
                var categoryExists = await _categoryRepository.DataExistsAsync(updatedEntity);
                if (!categoryExists)
                {
                    return new Result<string>(false, CategoryErrorMessages.DoesNotExist);
                }

                await _categoryRepository.UpdateAsync(updatedEntity);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                _logger.LogError($"{nameof(invalidOperationException)} thrown when updating a Category", invalidOperationException);

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError($"{nameof(dbUpdateException)} thrown when updating a Category", dbUpdateException);

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }
            catch (SqlException sqlException)
            {
                _logger.LogError($"{nameof(sqlException)} thrown when updating a Category", sqlException);

                return new Result<string>(false, DatabaseErrorMessages.DatabaseOperationError);
            }

            return new Result<string>(true, string.Empty);
        }
    }
}
