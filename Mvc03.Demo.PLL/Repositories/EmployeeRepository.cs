using Mvc.Demo.DAL.Data.Contexts;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mvc03.Demo.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext _dbContext;
        public EmployeeRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetEmployee(int? Id)
        {
            return _dbContext.Employees.Find(Id);
        }

        public int Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
