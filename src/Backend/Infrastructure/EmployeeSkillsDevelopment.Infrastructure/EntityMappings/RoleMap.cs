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
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(s => s.RoleId);
            builder.Property(s => s.RoleName).HasColumnName("RoleName").HasMaxLength(50).IsRequired();
            builder.HasIndex(s => s.RoleName).IsUnique();

        }
    }
}
