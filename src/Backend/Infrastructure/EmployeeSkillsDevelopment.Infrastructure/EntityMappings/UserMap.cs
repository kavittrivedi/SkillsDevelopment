using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillsDevelopment.Infrastructure.EntityMappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(s => s.UserId);
            builder.Property(s => s.ObjectId).HasColumnName("ObjectId").IsRequired();
            builder.Property(s => s.UserName).HasColumnName("UserName").HasMaxLength(50).IsRequired();
            builder.Property(s => s.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
            builder.Property(s => s.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            builder.HasIndex(s => s.ObjectId).IsUnique();

        }
    }

}
