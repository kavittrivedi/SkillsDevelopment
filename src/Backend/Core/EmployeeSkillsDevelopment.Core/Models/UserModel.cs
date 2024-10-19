using EmployeeSkillsDevelopment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Models
{
    public class UserModel
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Object id is required")]
        public string ObjectId { get; set; } = string.Empty;

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
