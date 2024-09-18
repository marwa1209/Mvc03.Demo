using Mvc.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetDepartment(int ?Id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
