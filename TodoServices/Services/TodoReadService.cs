using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoModels.Core.DataModels;
using TodoModels.Core.Enums;
using TodoServices.Interfaces;

namespace TodoServices.Services
{
    public class TodoReadService : ITodoReadService
    {
        protected readonly ICrudService<Todo> _todoService;

        public TodoReadService(ICrudService<Todo> todoService)
        {
            _todoService = todoService;
        }

        public async Task<IQueryable<Todo>> ReadByCategory(Guid categoryId)
        {
            var todoList = await _todoService.Read();
            return todoList.Where(x => x.CategoryId == categoryId);
        }

        public async Task<IQueryable<Todo>> ReadOpenTodosAndLatests(int readLatestIntervalInHours)
        {
            var todoList = await _todoService.Read();

            return todoList
                .Where(x => x.Status != StatusEnum.Completed || x.LastModified >= DateTime.Now.AddHours(-1 * readLatestIntervalInHours))
                .OrderByDescending(x => x.LastModified);
        }

        public async Task<Dictionary<string, List<Todo>>> ReadGroupByCategory()
        {
            var todoList = await _todoService.Read();
            List<CategoryGroupModel> categoryGroupModelList = new List<CategoryGroupModel>();

            var queryGroupByCategory = todoList.GroupBy(x => x.Category.Bug).OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.ToList());

            return queryGroupByCategory;
        }

        public async Task<IEnumerable<TreeModel>> ReadTree()
        {
            var todoList = await _todoService.Read();

            Dictionary<Guid, TreeModel> treeDataStructure = new Dictionary<Guid, TreeModel>();
            todoList.ToList().ForEach(x => treeDataStructure.Add((Guid)x.TodoId, new TreeModel { NodeTodo = x }));

            foreach (var item in treeDataStructure.Values)
            {
                if (item.NodeTodo.ParentId != null)
                {
                    treeDataStructure[(Guid)item.NodeTodo.ParentId.Value].ChildItems.Add(item);
                }
            }

            return treeDataStructure.Values.Where(x => x.NodeTodo.ParentId == null);
        }
    }
}
