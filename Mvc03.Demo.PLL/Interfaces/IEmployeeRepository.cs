﻿using Mvc.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
       Task<IEnumerable<Employee>> GetByNameAsync(string name);
    }
}
