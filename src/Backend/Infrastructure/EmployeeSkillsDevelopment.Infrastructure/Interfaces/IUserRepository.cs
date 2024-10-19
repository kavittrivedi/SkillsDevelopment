using EmployeeSkillsDevelopment.Infrastructure.Models;

namespace EmployeeSkillsDevelopment.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByObjectId(string objectId);
        bool AddUser(User user);
        UserRole? GetRoleByObjectId(string objectId);

    }
}
