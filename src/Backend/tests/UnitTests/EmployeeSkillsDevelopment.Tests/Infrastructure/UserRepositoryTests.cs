using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;

namespace UserSkillsDevelopment.Tests.Infrastructure
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly Mock<IAppDbContext> _mockAppDbContext;
        private readonly IFixture _fixture;

        public UserRepositoryTests()
        {
            _mockAppDbContext = new Mock<IAppDbContext>();
            _fixture = new Fixture();
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            return mockDbSet;
        }

        private List<User> CreateUserList(int count = 2)
        {
            var users = _fixture.Build<User>()
                .With(u => u.ObjectId) // Assuming all users have the same ObjectId for this test
                .Without(u => u.UserRoles)
                .CreateMany(count - 1)
                .ToList();

            var specificUser = _fixture.Build<User>()
            .With(u => u.ObjectId, "1") // Set specific ObjectId
            .Without(u => u.UserRoles)
            .Create();

            var randomIndex = new Random().Next(users.Count + 1);
            users.Insert(randomIndex, specificUser);

            return users;

        }

        private User CreateUser()
        {
            return _fixture.Build<User>()
                .With(u => u.ObjectId, "1")
                .Without(u => u.UserRoles)
                .Create();
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void GetUserByObjectId_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var id = "1";
            var users = CreateUserList(3).AsQueryable();
            var mockDbSet = CreateMockDbSet(users);
            _mockAppDbContext.Setup(c => c.Users).Returns(mockDbSet.Object);
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetUserByObjectId(id);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(id, actual.ObjectId);
            _mockAppDbContext.Verify(c => c.Users, Times.Once);
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void GetUserByObjectId_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var id = "";
            var users = CreateUserList().AsQueryable();
            var mockDbSet = CreateMockDbSet(users);
            _mockAppDbContext.Setup(c => c.Users).Returns(mockDbSet.Object);
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetUserByObjectId(id);

            // Assert
            Assert.Null(actual);
            _mockAppDbContext.Verify(c => c.Users, Times.Once);
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void AddUser_ReturnsTrue_WhenUserAddedSuccessfully()
        {
            // Arrange
            var user = CreateUser();
            var mockDbSet = new Mock<DbSet<User>>();
            _mockAppDbContext.SetupGet(c => c.Users).Returns(mockDbSet.Object);
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.AddUser(user);

            // Assert
            Assert.True(actual);
            mockDbSet.Verify(c => c.Add(user), Times.Once);
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void AddUser_ReturnsFalse_WhenUserIsNotAdded()
        {
            // Arrange
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.AddUser(null);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void GetRoleByObjectId_ReturnsUserRole_WhenUserRoleExists()
        {
            // Arrange
            var objectId = "1";
            var userRole = _fixture.Build<UserRole>()
                .With(ur => ur.IsActive, true)
                .With(ur => ur.User, new User { ObjectId = objectId })
                .With(ur => ur.Role, new Role { RoleName = "Admin" })
                .Create();

            var userRoles = new List<UserRole> { userRole }.AsQueryable();
            var mockDbSet = CreateMockDbSet(userRoles);
            _mockAppDbContext.Setup(c => c.UserRoles).Returns(mockDbSet.Object);
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetRoleByObjectId(objectId);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(objectId, actual.User.ObjectId);
            _mockAppDbContext.Verify(c => c.UserRoles, Times.Once);
        }

        [Fact]
        [Trait("User", "UserRepositoryTests")]
        public void GetRoleByObjectId_ReturnsNull_WhenUserRoleDoesNotExist()
        {
            // Arrange
            var objectId = "1";
            var userRoles = new List<UserRole>().AsQueryable();
            var mockDbSet = CreateMockDbSet(userRoles);
            _mockAppDbContext.Setup(c => c.UserRoles).Returns(mockDbSet.Object);
            var repository = new UserRepository(_mockAppDbContext.Object);

            // Act
            var actual = repository.GetRoleByObjectId(objectId);

            // Assert
            Assert.Null(actual);
            _mockAppDbContext.Verify(c => c.UserRoles, Times.Once);
        }

        public void Dispose()
        {
            _mockAppDbContext.VerifyAll();
        }
    }
}
