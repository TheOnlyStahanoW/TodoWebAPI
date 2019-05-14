using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TodoModels.Core.Enums;
using TodoModels.Core.Settings;
using TodoServices.Utilites;

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
        public DateTime Created { get; set; }
        [StringLength(120)]
        public string Creator { get; set; }
        public DateTime? LastModified { get; set; }
        [StringLength(120)]
        public string Modifier { get; set; }
        public int WorkHoursLeft {
            get
            {
                //Egyelőre be van égetve a két szám, nem tudom hogyan lehetne hatékonyabban eltárolni a kezdés és vég időpontokat a configból ide.
                return WorkHoursCalculation.GetWorkHoursSum(DateTime.Now, Deadline, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0));
            }
        }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
