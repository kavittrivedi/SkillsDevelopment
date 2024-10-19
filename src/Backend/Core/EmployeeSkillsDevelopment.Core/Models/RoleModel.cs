using EmployeeSkillsDevelopment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Models
{
    public class RoleModel
    {
        [Required(ErrorMessage = "Role id is required")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; } = string.Empty;

        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();

    }
}
