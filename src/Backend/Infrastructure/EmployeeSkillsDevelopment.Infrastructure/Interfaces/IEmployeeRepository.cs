using EmployeeSkillsDevelopment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Infrastructure.Interfaces
{
    /// <summary>
    /// EmployeeRepository interface
    /// </summary>
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees(int page, int pageSize);

        int TotalEmployeesCount();
    }

}
