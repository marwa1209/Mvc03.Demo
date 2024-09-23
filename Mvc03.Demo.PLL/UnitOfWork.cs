using Mvc.Demo.DAL.Data.Contexts;
using Mvc03.Demo.BLL.Interfaces;
using Mvc03.Demo.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        public UnitOfWork(AppDBContext cntext)
        {
           _employeeRepository = new EmployeeRepository(cntext);
           _departmentRepository = new DepartmentRepository(cntext);
            _context = cntext;
        }
        public IDepartmentRepository DepartmentRepository =>_departmentRepository;

        public IEmployeeRepository EmployeeRepository =>_employeeRepository;
    }
}
