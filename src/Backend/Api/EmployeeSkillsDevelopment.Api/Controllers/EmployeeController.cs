using EmployeeSkillsDevelopment.Api.Configurations;
using EmployeeSkillsDevelopment.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web.Resource;

namespace EmployeeSkillsDevelopment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    [ApiExplorerSettings(GroupName = "Employee")]

    //[ServiceFilter(typeof(ExceptionFilter))]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;

        public EmployeeController(IEmployeeService employeeService, IConfiguration configuration)
        {
            _employeeService = employeeService;
            _configuration = configuration;
        }

        /// <summary>
        /// Return list of employee
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("GetAllEmployees")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public IActionResult GetAllEmployees(int? page = null, int? pageSize= null)
        {
            var paginationSettings = _configuration.GetSection("Pagination").Get<PaginationSettings>();
            int effectivePage = page ?? paginationSettings.DefaultPage;
            int effectivePageSize = pageSize ?? paginationSettings.DefaultPageSize;
            var response = _employeeService.GetAllEmployees(effectivePage, effectivePageSize);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get count of total employee
        /// </summary>
        /// <returns></returns>

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("GetTotalEmployeesCount")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public IActionResult TotalEmployeesCount()
        {
            var response = _employeeService.TotalEmployeesCount();

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}
