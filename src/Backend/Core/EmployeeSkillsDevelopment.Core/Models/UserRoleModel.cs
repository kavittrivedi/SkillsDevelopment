using EmployeeSkillsDevelopment.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillsDevelopment.Core.Models
{
    public class UserRoleModel
    {
        [Required(ErrorMessage = "User role id is required")]
        public int UserRoleId { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role id is required")]
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; } = new User();

        public Role Role { get; set; } = new Role();
    }
}
