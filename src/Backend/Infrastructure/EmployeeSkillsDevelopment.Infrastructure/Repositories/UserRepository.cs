using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkillsDevelopment.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppDbContext _appDbContext;

        public UserRepository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User? GetUserByObjectId(string objectId)
        {

            var user = _appDbContext.Users
                .FirstOrDefault(u => u.ObjectId == objectId);
            return user;
        }

        public bool AddUser(User user)
        {
            var result = false;
            if (user != null)
            {
                _appDbContext.Users.Add(user);
                result = true;
            }
            return result;
        }

        public UserRole? GetRoleByObjectId(string objectId)
        {

            var role = _appDbContext.UserRoles
                .Include(u => u.Role).Include(c => c.User)
                .Where(u => u.IsActive)
                .FirstOrDefault(u => u.User.ObjectId == objectId);
            return role;
        }

        

       
    }
}
