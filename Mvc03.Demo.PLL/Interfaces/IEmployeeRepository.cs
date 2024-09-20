using Mvc.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetEmployee(int? Id);
        int Add(Employee entity);
        int Update(Employee entity);
        int Delete(Employee entity);
    }
}
