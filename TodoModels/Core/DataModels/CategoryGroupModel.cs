using System;
using System.Collections.Generic;
using System.Text;

namespace TodoModels.Core.DataModels
{
    public class CategoryGroupModel
    {
        public Guid CategoryId { get; set; }
        public List<Todo> TodoItems { get; set; } = new List<Todo>();
    }
}
