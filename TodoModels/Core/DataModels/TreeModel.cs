using System;
using System.Collections.Generic;
using System.Text;

namespace TodoModels.Core.DataModels
{
    public class TreeModel
    {
        public Todo NodeTodo { get; set; }
        public List<TreeModel> ChildItems { get; set; } = new List<TreeModel>();
    }
}
