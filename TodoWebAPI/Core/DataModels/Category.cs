using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Core.DataModels
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [StringLength(250)]
        public string Bug { get; set; }
        public string Task { get; set; }
        public bool Epic { get; set; } = false;
    }
}
