using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillsDevelopment.Api.DTOs
{
    public class AddUserDto
    {

        [Required(ErrorMessage = "Object id is required")]
        public string ObjectId { get; set; } = string.Empty;

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
