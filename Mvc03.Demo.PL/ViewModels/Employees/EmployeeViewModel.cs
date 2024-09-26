﻿using Mvc.Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Mvc03.Demo.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
       // [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [Range(25, 60, ErrorMessage = "Age Must Be Between 25 and 60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public int? WorkForId { get; set; }//FK
        public Department? WorkFor { get; set; } //Navifitional Property -Optional
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
