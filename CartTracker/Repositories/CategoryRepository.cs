using CartTracker.Database;
using CartTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartTracker.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly CartTrackerContext _context;

        public CategoryRepository(CartTrackerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            IQueryable<Category> categories = _context.Categories.FromSql("EXEC GetAllCategories");

            return await categories.ToListAsync();
        }
    }
}
