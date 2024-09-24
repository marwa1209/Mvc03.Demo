﻿using Microsoft.EntityFrameworkCore;
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
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeRepository
    {
        public EmployeeRepository(AppDBContext dbContext):base(dbContext) 
        {
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _dbContext.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.WorkFor).ToListAsync();
        }
    }
}
