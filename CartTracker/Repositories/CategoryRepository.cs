﻿using CartTracker.Database;
using CartTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

        public async Task<bool> DataExistsAsync(Category entityToCheck)
        {
            IQueryable<Category> categories = _context.Categories
                .Where(category => category.Name == entityToCheck.Name);

            return await categories.AnyAsync();
        }

        public async Task AddAsync(Category newEntity)
        {
            var nameParameter = new SqlParameter("@CategoryName", newEntity.Name);
            await _context.Database.ExecuteSqlCommandAsync("AddCategory @CategoryName", nameParameter);
        }
    }
}
