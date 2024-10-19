

namespace EmployeeSkillsDevelopment.Infrastructure.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string ObjectId { get; set; }=string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
