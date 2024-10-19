using EmployeeSkillsDevelopment.Infrastructure.EntityMappings;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeSkillsDevelopment.Infrastructure.Data
{
    /// <summary>
    /// DbContext implementation
    /// IAppDbContext implementation
    /// </summary>
    public class AppDbContext: DbContext,IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public EntityState GetEntryState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        public void SetEntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class
        {
            Entry(entity).State = entityState;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
        }

    }
}
