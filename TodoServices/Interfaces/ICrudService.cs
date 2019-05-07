using System;
using System.Linq;
using System.Threading.Tasks;

namespace TodoServices.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        Task<T> Create(T item);
        Task<T> Update(Guid id, T item);
        Task<T> Delete(Guid id);
        Task<IQueryable<T>> Read();
        Task<T> Read(Guid id);
        Task<T> ReadNotTracked(Guid id);
    }
}