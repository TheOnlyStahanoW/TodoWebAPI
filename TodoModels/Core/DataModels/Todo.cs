using System;
using System.ComponentModel.DataAnnotations;
using TodoModels.Core.Enums;

namespace TodoModels.Core.DataModels
{
    public class Todo
    {
        [Key]
        public Guid TodoId { get; set; }
        public Guid? CategoryId { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public PriorityEnum Priority { get; set; } = PriorityEnum.Normal;
        [StringLength(120)]
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Started;
        public Category Category { get; set; }
        public Guid? ParentId { get; set; }
        public bool Deleted { get; set; } = false;
        public DateTime? Created { get; set; }
        [StringLength(120)]
        public string Creator { get; set; }
        public DateTime? LastModified { get; set; }
        [StringLength(120)]
        public string Modifier { get; set; }
    }
}
