using AutoFixture;
using AutoMapper;
using EmployeeSkillsDevelopment.Core.Services;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Moq;

namespace EmployeeSkillsDevelopment.Tests.Repositories
{
    public class EmployeeServiceTests : IDisposable
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EmployeeService _service;
        private readonly IFixture _fixture;


        public EmployeeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new EmployeeService(_mockUnitOfWork.Object, _mockMapper.Object);
            _fixture = new Fixture();

        }

        [Fact]
        [Trait("Employee", "EmployeeServiceTests")]
        public void GetAllEmployees_ShouldReturnCorrectEmployees()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;

            var mockEmployees = CreateEmployeeList().AsQueryable();

            _mockUnitOfWork.Setup(r => r._employeeRepository.GetAllEmployees(page, pageSize))
                .Returns(mockEmployees);

            // Act
            var result = _service.GetAllEmployees(page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Employees retrieved successfully!", result.Message);
            Assert.NotNull(result.Data);
            _mockUnitOfWork.Verify(r => r._employeeRepository.GetAllEmployees(page, pageSize), Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeServiceTests")]
        public void GetAllEmployees_ShouldReturnSuccessFalse_WhenEmployeesNotFound()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;

            var mockEmployees = Enumerable.Empty<Employee>().AsQueryable();

            _mockUnitOfWork.Setup(r => r._employeeRepository.GetAllEmployees(page, pageSize))
                .Returns(mockEmployees);

            // Act
            var result = _service.GetAllEmployees(page, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("No record found", result.Message);
            Assert.Null(result.Data);
            _mockUnitOfWork.Verify(r => r._employeeRepository.GetAllEmployees(page, pageSize), Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeServiceTests")]
        public void TotalEmployeesCount_ReturnsSuccessfulResponse()
        {
            // Arrange
            const int expectedTotalInterviewSlots = 10;

            _mockUnitOfWork.Setup(r => r._employeeRepository.TotalEmployeesCount())
                .Returns(expectedTotalInterviewSlots);

            // Act
            var result = _service.TotalEmployeesCount();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(expectedTotalInterviewSlots, result.Data);
            _mockUnitOfWork.Verify(r => r._employeeRepository.TotalEmployeesCount(), Times.Once);
        }

        private List<Employee> CreateEmployeeList()
        {
            return _fixture.CreateMany<Employee>(2).ToList();
        }

        public void Dispose()
        {
            _mockUnitOfWork.VerifyAll();
            _mockMapper.VerifyAll();
        }
    }
}
