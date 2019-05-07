using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoModels.Core.DataModels;

namespace TodoServices.Interfaces
{
    public interface ITodoService<T> where T : Todo
    {
        Task<T> Create(T item);
        Task<T> Update(Guid id, T item);
        Task<T> Delete(Guid id);
        Task<IEnumerable<T>> Read();
        Task<T> Read(Guid id);
        Task<T> ReadNotTracked(Guid id);
    }
}