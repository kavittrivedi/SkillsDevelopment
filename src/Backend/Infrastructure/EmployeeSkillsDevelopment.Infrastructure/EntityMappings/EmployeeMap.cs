using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.EntityMappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(s => s.EmployeeId);
            builder.Property(s => s.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
            builder.Property(s => s.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            builder.Property(s => s.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();

        }
    }
}
