using EMSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMSystem.Domain.Model
{
    public class Employee : IEntity
    {   [Key]
        public int id { get; set; }
        //public Guid Id { get; set; }
        [Display(Name ="first name")]
        public string fstname { get; set; }
        [Display(Name ="last name")]
        public string lstname { get; set; }
        public int age { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(Name ="Disable"),DefaultValue(false)]
        public bool dis { get; set; }
    }
}
