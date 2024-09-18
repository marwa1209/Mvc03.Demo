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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDBContext _dbContext;//null
        public DepartmentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;  
        }
        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }
        public Department? GetDepartment(int? Id)
        {
           // return _dbContext.Departments.FirstOrDefault(D=>D.Id==Id);
            return _dbContext.Departments.Find(Id);
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
