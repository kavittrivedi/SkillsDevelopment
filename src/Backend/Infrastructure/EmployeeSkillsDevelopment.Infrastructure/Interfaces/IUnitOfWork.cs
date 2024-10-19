using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository _userRepository { get; }
        IEmployeeRepository _employeeRepository { get; }
        public bool SaveChanges();

    }
}
