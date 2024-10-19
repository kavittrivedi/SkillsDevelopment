﻿

namespace EmployeeSkillsDevelopment.Infrastructure.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();

    }
}
