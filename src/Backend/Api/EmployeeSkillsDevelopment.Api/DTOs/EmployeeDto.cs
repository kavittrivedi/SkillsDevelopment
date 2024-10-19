using System.ComponentModel.DataAnnotations;

namespace EmployeeSkillsDevelopment.Api.DTOs
{
    public class EmployeeDto
    {
        [Required(ErrorMessage ="Employee id is required")]    
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
    }
}
