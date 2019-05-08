using System;
using System.Collections.Generic;
using System.Text;

namespace TodoModels.Core.DataModels
{
    public class TodoHeader
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; }
    }
}
