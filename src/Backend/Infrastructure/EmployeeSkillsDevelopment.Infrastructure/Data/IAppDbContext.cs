using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.Data
{
    /// <summary>
    /// AppDbContext interface
    /// IDbContext implementation   
    /// </summary>
    public interface IAppDbContext: IDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
