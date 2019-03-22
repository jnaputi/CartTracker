using CartTracker.Models;
using CartTracker.Repositories;
using CartTracker.Results;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CartTracker.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<QueryResult<ICollection<Category>>> GetCategoriesAsync()
        {
            ICollection<Category> categories = null;

            try
            {
                categories = await _categoryRepository.GetAllAsync();
            }
            catch(SqlException e)
            {
                return new QueryResult<ICollection<Category>>(categories, "There was an error with the database.  Please try again later");
            }

            return new QueryResult<ICollection<Category>>(categories);
        }
    }
}
