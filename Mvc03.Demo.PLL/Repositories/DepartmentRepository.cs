using Mvc.Demo.DAL.Data.Contexts;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL.Repositories
{
    //clr will create object in run time
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AppDBContext dbContext):base(dbContext)
        {
        }

    }
}
