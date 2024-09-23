using AutoMapper;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.PL.ViewModels.Employees;

namespace Mvc03.Demo.PL.Mapping.Employees
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
