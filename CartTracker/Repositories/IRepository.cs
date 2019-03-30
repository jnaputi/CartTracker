using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartTracker.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<ICollection<TModel>> GetAllAsync();
        Task<bool> DataExistsAsync(TModel entityToCheck);
        Task AddAsync(TModel newEntity);
        Task UpdateAsync(TModel updatedEntity);
    }
}
