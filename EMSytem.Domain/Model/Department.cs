using EMSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMSystem.Domain.Model
{
    public class Department : IEntity
    {
        [Key]
        public int id { get; set; }
        //public Guid Id { get; set; }
        [Required]
        public string name { get; set; }
        public string desc { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Disable"), DefaultValue(false)]
        public bool dis { get; set; }
    }
}
