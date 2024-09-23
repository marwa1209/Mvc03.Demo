using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Demo.DAL.Models
{
    public class Department:Base
    {
        [Required(ErrorMessage ="Code Is Required")]
        public string Code { get; set; }

        public ICollection<Employee>? Employees { get; set; }

    }
}
