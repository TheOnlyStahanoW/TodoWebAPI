using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TodoServices.Interfaces;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;

namespace TodoServices.Services
{
    public class CategoryService : ICrudService<Category>
    {
        protected readonly ApiDbContext _apiDbContext;

        public CategoryService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Category> Create(Category category)
        {
            _apiDbContext.Categories.Add(category);
            await _apiDbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(Guid id, Category category)
        {
            _apiDbContext.Entry(category).State = EntityState.Modified;

            await _apiDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(Guid id)
        {
            var category = await _apiDbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            _apiDbContext.Categories.Remove(category);
            await _apiDbContext.SaveChangesAsync();

            return category;
        }

        public Task<IQueryable<Category>> Read()
        {
            return Task.FromResult(_apiDbContext.Categories.AsQueryable());
        }

        public async Task<Category> Read(Guid id)
        {
            return await _apiDbContext.Categories.FindAsync(id);
        }

        public async Task<Category> ReadNotTracked(Guid id)
        {
            return await _apiDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        private bool TodoExists(Guid id)
        {
            return _apiDbContext.Categories.Any(e => e.CategoryId == id);
        }
    }
}
