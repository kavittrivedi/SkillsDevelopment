using AutoFixture;
using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EmployeeSkillsDevelopment.Tests.Services
{
    public class EmployeeRepositoryTests : IDisposable
    {
        private readonly Mock<IAppDbContext> _mockAppDbContext;
        private readonly IFixture _fixture;

        public EmployeeRepositoryTests()
        {
            _mockAppDbContext = new Mock<IAppDbContext>();
            _fixture = new Fixture(); 

        }
            
        private Mock<DbSet<Employee>> CreateMockDbSet(IQueryable<Employee> employees)
        {
            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(c => c.Expression).Returns(employees.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(c => c.Provider).Returns(employees.Provider);
            return mockDbSet;
        }

        [Fact]
        [Trait("Employee", "EmployeeRepositoryTests")]
        public void GetAllEmployees_ReturnsEmployees_WhenEmployeesExist()
        {
            // Arrange
            var employees = CreateEmployeeList().AsQueryable();

            var mockDbSet = CreateMockDbSet(employees);
            _mockAppDbContext.Setup(c => c.Employees).Returns(mockDbSet.Object);

            var repository = new EmployeeRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetAllEmployees(1, 2);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count());
            _mockAppDbContext.Verify(c => c.Employees, Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeRepositoryTests")]
        public void GetAllEmployees_ReturnsEmpty_WhenNoEmployeesExist()
        {
            // Arrange
            var employees = new List<Employee>().AsQueryable();
            var mockDbSet = CreateMockDbSet(employees);
            _mockAppDbContext.Setup(c => c.Employees).Returns(mockDbSet.Object);

            var repository = new EmployeeRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetAllEmployees(1, 2);

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
            _mockAppDbContext.Verify(c => c.Employees, Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeRepositoryTests")]
        public void TotalEmployeesCount_ReturnsCount_WhenEmployeesExist()
        {
            // Arrange
            var employees = CreateEmployeeList().AsQueryable();

            var mockDbSet = CreateMockDbSet(employees);
            _mockAppDbContext.Setup(c => c.Employees).Returns(mockDbSet.Object);

            var repository = new EmployeeRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.TotalEmployeesCount();

            // Assert
            Assert.Equal(employees.Count(), actual);
            _mockAppDbContext.Verify(c => c.Employees, Times.Once);
        }

        [Fact]
        [Trait("Employee", "EmployeeRepositoryTests")]
        public void TotalEmployeesCount_ReturnsZero_WhenNoEmployeesExist()
        {
            // Arrange
            var employees = new List<Employee>().AsQueryable();
            var mockDbSet = CreateMockDbSet(employees);
            _mockAppDbContext.Setup(c => c.Employees).Returns(mockDbSet.Object);

            var repository = new EmployeeRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.TotalEmployeesCount();

            // Assert
            Assert.Equal(employees.Count(), actual);
            _mockAppDbContext.Verify(c => c.Employees, Times.Once);
        }

        private List<Employee> CreateEmployeeList()
        {
            return _fixture.CreateMany<Employee>(2).ToList();
        }

        public void Dispose()
        {
            _mockAppDbContext.VerifyAll();
        }
    }
}
