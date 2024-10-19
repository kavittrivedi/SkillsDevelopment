using AutoMapper;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;

namespace EmployeeSkillsDevelopment.Core.Services
{
    /// <summary>
    /// IEmployeeService implementation
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ServiceResponse<IEnumerable<EmployeeModel>> GetAllEmployees(int page, int pageSize)
        {
            var response = new ServiceResponse<IEnumerable<EmployeeModel>>();

            var employees = _uow._employeeRepository.GetAllEmployees(page, pageSize);
            if (employees != null && employees.Any())
            {
                response.Data = _mapper.Map<IEnumerable<EmployeeModel>>(employees);
                response.Message = "Employees retrieved successfully!";
            }

            else
            {
                response.Success = false;
                response.Message = "No record found";
            }
            return response;
        }

        public ServiceResponse<int> TotalEmployeesCount()
        {
            var response = new ServiceResponse<int>();

            int totalEmployees = _uow._employeeRepository.TotalEmployeesCount();

            response.Data = totalEmployees;
            return response;

        }


    }
}
