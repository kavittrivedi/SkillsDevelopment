using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.EntityMappings
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(s => s.UserRoleId);
            builder.Property(s => s.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(s => s.RoleId).HasColumnName("RoleId").IsRequired();
            builder.Property(s => s.IsActive).HasColumnName("IsActive").IsRequired();
            // User relationship
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            // Role relationship
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
