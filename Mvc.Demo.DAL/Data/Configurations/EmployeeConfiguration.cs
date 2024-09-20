using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mvc.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Demo.DAL.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>

    {
        void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
