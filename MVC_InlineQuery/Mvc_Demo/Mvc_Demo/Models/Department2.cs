using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Demo_EF.Models
{
    public class Department2
    {
        public int Id { get; set; }
        public string DeptName { get; set; }

        public virtual ICollection<Employee2> Employees { get; set; }
    }
}