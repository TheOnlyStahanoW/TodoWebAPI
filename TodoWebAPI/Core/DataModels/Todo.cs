using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWebAPI.Core.DataModels
{
    public class Todo
    {
        public Guid TodoId { get; set; } = Guid.NewGuid();
        public int? CategoryId { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public PriorityEnum Priority { get; set; } = PriorityEnum.Normal;
        [StringLength(120)]
        public string Responsible { get; set; }
        public DateTime Deadline { get; set; }
        [StringLength(50)]
        public StatusEnum Status { get; set; } = StatusEnum.Started;
        public Category Category { get; set; }
        public Guid? ParentId { get; set; }
        public bool Deleted { get; set; } = false;
        [Required]
        public DateTime Created { get; set; }
        [Required]
        [StringLength(120)]
        public string Creator { get; set; }
        public DateTime LastModified { get; set; }
        [StringLength(120)]
        public string Modifier { get; set; }
    }

    public enum PriorityEnum
    {
        Low,
        Normal,
        High,
        ASAP
    }

    public enum StatusEnum
    {
        Started,
        InProgress,
        OnHalt,
        Completed,
        Dropped
    }
}
