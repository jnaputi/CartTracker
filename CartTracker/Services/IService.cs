using System.Collections.Generic;
using System.Threading.Tasks;
using CartTracker.Results;

namespace CartTracker.Services
{
    public interface IService<TModel>
    {
        Task<IResult<QueryResultData<ICollection<TModel>>>> GetAllAsync();
        Task<IResult<string>> AddAsync(TModel entityToAdd);
        Task<IResult<string>> UpdateAsync(TModel updatedEntity);
    }
}