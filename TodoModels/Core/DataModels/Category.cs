using System;
using System.ComponentModel.DataAnnotations;

namespace TodoModels.Core.DataModels
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
