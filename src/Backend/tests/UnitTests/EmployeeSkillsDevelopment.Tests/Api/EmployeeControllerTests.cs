using AutoFixture;
using EmployeeSkillsDevelopment.Api.Configurations;
using EmployeeSkillsDevelopment.Api.Controllers;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;


namespace EmployeeSkillsDevelopment.Tests.Controllers
{
    public class EmployeeControllerTests : IDisposable
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly EmployeeController _controller;
        private readonly PaginationSettings _paginationSettings;
        private readonly IFixture _fixture;


        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _fixture = new Fixture();
            _mockConfiguration = new Mock<IConfiguration>();

            // Setup common configuration
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(s => s["DefaultPage"]).Returns("1");
            mockSection.Setup(s => s["DefaultPageSize"]).Returns("4");
            _mockConfiguration.Setup(c => c.GetSection("Pagination")).Returns(mockSection.Object);

            _paginationSettings = new PaginationSettings
            {
                DefaultPage = int.Parse(mockSection.Object["DefaultPage"]),
                DefaultPageSize = int.Parse(mockSection.Object["DefaultPageSize"])
            };

            _controller = new EmployeeController(_mockEmployeeService.Object, _mockConfiguration.Object);
        }

        [Fact]
        [Trait("Employee", "EmployeeControllerTests")]

        public void GetAllEmployees_ReturnsOkWithEmployees_WhenPageSettingsAreProvided()
        {
            // Arrange
            var employees = CreateEmployeeList();

            var response = new ServiceResponse<IEnumerable<EmployeeModel>>
            {
                Success = true,
                Data = employees.Select(e => new EmployeeModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                })
            };

            int? page = 1;
            int effectivePage = page ?? _paginationSettings.DefaultPage;
            int? pageSize = 2;
            int effectivePageSize = pageSize ?? _paginationSettings.DefaultPageSize;

            _mockEmployeeService.Setup(s => s.GetAllEmployees(effectivePage, effectivePageSize)).Returns(response);

            // Act
            var actual = _controller.GetAllEmployees(page, pageSize) as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockEmployeeService.Verify(s => s.GetAllEmployees(effectivePage, effectivePageSize), Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeControllerTests")]
        public void GetAllEmployees_ReturnsNotFound_WhenNoEmployeesExist()
        {
            // Arrange
            var response = new ServiceResponse<IEnumerable<EmployeeModel>>
            {
                Success = false,
                Data = null
            };

            int? page = 1;
            int effectivePage = page ?? _paginationSettings.DefaultPage;
            int? pageSize = 2;
            int effectivePageSize = pageSize ?? _paginationSettings.DefaultPageSize;

            _mockEmployeeService.Setup(s => s.GetAllEmployees(effectivePage, effectivePageSize)).Returns(response);

            // Act
            var actual = _controller.GetAllEmployees(page, pageSize) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockEmployeeService.Verify(s => s.GetAllEmployees(effectivePage, effectivePageSize), Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeControllerTests")]
        public void TotalEmployeesCount_ReturnsOkWithCount()
        {
            // Arrange
            var response = new ServiceResponse<int>
            {
                Success = true,
                Data = 2 // Example count
            };

            _mockEmployeeService.Setup(s => s.TotalEmployeesCount()).Returns(response);

            // Act
            var actual = _controller.TotalEmployeesCount() as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockEmployeeService.Verify(s => s.TotalEmployeesCount(), Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeControllerTests")]
        public void TotalEmployeesCount_ReturnsNotFound()
        {
            // Arrange
            var response = new ServiceResponse<int>
            {
                Success = false,
                Data = 0
            };

            _mockEmployeeService.Setup(s => s.TotalEmployeesCount()).Returns(response);

            // Act
            var actual = _controller.TotalEmployeesCount() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(404, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockEmployeeService.Verify(s => s.TotalEmployeesCount(), Times.Once);
        }

        private List<Employee> CreateEmployeeList()
        {
            return _fixture.CreateMany<Employee>(2).ToList();
        }

        public void Dispose()
        {
            _mockEmployeeService.VerifyAll();
        }
    }
}
