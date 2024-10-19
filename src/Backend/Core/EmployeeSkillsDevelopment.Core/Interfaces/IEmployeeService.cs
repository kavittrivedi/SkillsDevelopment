using EmployeeSkillsDevelopment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Interfaces
{
    /// <summary>
    /// EmployeeService interface
    /// </summary>
    public interface IEmployeeService
    {
        ServiceResponse<IEnumerable<EmployeeModel>> GetAllEmployees(int page, int pageSize);

        ServiceResponse<int> TotalEmployeesCount();
    }
}
