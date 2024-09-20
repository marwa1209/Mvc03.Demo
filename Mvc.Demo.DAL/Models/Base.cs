using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Demo.DAL.Models
{
    public class Base
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
