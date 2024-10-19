using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Models;
namespace EmployeeSkillsDevelopment.Infrastructure.Repositories
{
    /// <summary>
    /// IEmployeeRepository implementation
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IAppDbContext _appDbContext;


        public EmployeeRepository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public IEnumerable<Employee> GetAllEmployees(int page, int pageSize)
        {

            int skip = (page - 1) * pageSize;
            return _appDbContext.Employees
                .Skip(skip)
                .Take(pageSize)
                .ToList();

        }

        public int TotalEmployeesCount()
        {

            IQueryable<Employee> query = _appDbContext.Employees;
            return query.Count();

        }



    }
}
