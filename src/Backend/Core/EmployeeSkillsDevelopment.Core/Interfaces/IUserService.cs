using EmployeeSkillsDevelopment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Interfaces
{
    public interface IUserService
    {
        ServiceResponse<string> AddUserIfNotExists(UserModel user);

        ServiceResponse<string> GetUserRole(string objectId);
    }
}
