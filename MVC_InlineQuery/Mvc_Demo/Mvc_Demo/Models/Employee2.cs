using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Demo_EF.Models
{
    public class Employee2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int? DepartmentId { get; set; }

       // public virtual Department Department { get; set; }
        public string DepartmentName { get; set; }
    }
}