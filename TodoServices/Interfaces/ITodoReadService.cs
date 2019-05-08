using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoModels.Core.DataModels;

namespace TodoServices.Interfaces
{
    public interface ITodoReadService
    {
        Task<IQueryable<Todo>> ReadByCategory(Guid categoryGuid);
        Task<IQueryable<Todo>> ReadOpenTodosAndLatests(int readLatestIntervalInHours);
        Task<Dictionary<string, List<Todo>>> ReadGroupByCategory();
        Task<IEnumerable<TreeModel>> ReadTree();
    }
}
