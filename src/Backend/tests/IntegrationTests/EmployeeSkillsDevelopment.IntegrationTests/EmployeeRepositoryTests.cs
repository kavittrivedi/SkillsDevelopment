using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeSkillsDevelopment.IntegrationTests
{
    public class EmployeeRepositoryTests : IDisposable
    {
        private readonly IAppDbContext _appDbContext;
        private readonly List<Employee> _testEmployee;
        public EmployeeRepositoryTests()
        {
            var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();


            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
              .UseSqlServer(configuration.GetConnectionString("mydb"))
              .Options;

            _appDbContext = new AppDbContext(dbContextOptions);
            // Seed data
            _testEmployee = SeedDatabase();

        }
        [Fact]
        public void GetAll_ReturnsAllEmployee()
        {
            var page = 1;
            var pageSize = 6;
            // Arrange
            var employeeRepository = new EmployeeRepository(_appDbContext);

            // Act
            IEnumerable<Employee> employees = employeeRepository.GetAllEmployees(page, pageSize);

            // Assert
            Assert.NotNull(employees);
            Assert.Equal(_testEmployee.Count, employees.Count());
        }
        [Fact]
        public void TotalEmployeesCount_ReturnsCorrectCount()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_appDbContext);

            // Act
            int totalEmployee = employeeRepository.TotalEmployeesCount();

            // Assert
            Assert.Equal(_testEmployee.Count, totalEmployee);
        }
        public void Dispose()
        {
            // Cleanup test data
            _appDbContext.Database.ExecuteSqlRaw("DELETE FROM Employees");
            _appDbContext.Dispose();
        }
        private List<Employee> SeedDatabase()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Employee 1", LastName = "Employee 1", Email = "emloyee1@gmail.com" },
               new Employee { FirstName = "Employee 2", LastName = "Employee 2", Email = "emloyee2@gmail.com" },
               new Employee { FirstName = "Employee 3", LastName = "Employee 3", Email = "emloyee3@gmail.com" },
               new Employee { FirstName = "Employee 4", LastName = "Employee 4", Email = "emloyee4@gmail.com" },
               new Employee { FirstName = "Employee 5", LastName = "Employee 5", Email = "emloyee5@gmail.com" },
               new Employee { FirstName = "Employee 6", LastName = "Employee 6", Email = "emloyee6@gmail.com" }
            };
            _appDbContext.Employees.AddRange(employees);
            _appDbContext.SaveChanges();

            return employees;
        }
    }
}