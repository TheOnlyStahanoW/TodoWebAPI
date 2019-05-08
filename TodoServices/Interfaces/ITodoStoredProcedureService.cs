using System.Linq;
using System.Threading.Tasks;
using TodoModels.Core.DataModels;

namespace TodoServices.Interfaces
{
    public interface ITodoStoredProcedureService
    {
        Task<IQueryable<TodoHeader>> ReadOnlyIdAndName();
    }
}
