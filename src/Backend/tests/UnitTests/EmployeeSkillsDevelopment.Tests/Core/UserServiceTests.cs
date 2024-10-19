
using AutoFixture;
using AutoMapper;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Core.Services;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Fare;
using Moq;
using System.Diagnostics.Metrics;

namespace EmployeeSkillsDevelopment.Tests.Core
{
    public class UserServiceTests : IDisposable
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;
        private readonly IFixture _fixture;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockMapper.Object);
            _fixture = new Fixture();

        }

        [Fact]
        public void GetUserRole_ReturnsRole_WhenExists()
        {
            // Arrange
            var objectId = "1";
            var userRole = _fixture.Build<UserRole>()
                .With(ur => ur.IsActive, true)
                .With(ur => ur.User, new User { ObjectId = objectId })
                .With(ur => ur.Role, new Role { RoleName = "Admin" })
                .Create();


            _mockUnitOfWork.Setup(r => r._userRepository.GetRoleByObjectId(objectId)).Returns(userRole);
            // Act
            var actual = _userService.GetUserRole(objectId);

            // Assert
            Assert.True(actual.Success);
            Assert.NotNull(actual.Data);
            Assert.Equal(userRole.Role.RoleName, actual.Data);
            _mockUnitOfWork.Verify(r => r._userRepository.GetRoleByObjectId(objectId), Times.Once);
        }
        
        [Fact]
        public void GetUserRole_ReturnsNotFound_WhenRoleDoesntExists()
        {
            // Arrange
            var objectId = "1";
            _mockUnitOfWork.Setup(r => r._userRepository.GetRoleByObjectId(objectId)).Returns<IEnumerable<User>>(null);
            // Act
            var actual = _userService.GetUserRole(objectId);

            // Assert
            Assert.False(actual.Success);
            Assert.Null(actual.Data);
            Assert.Equal("User not found!", actual.Message);
            _mockUnitOfWork.Verify(r => r._userRepository.GetRoleByObjectId(objectId), Times.Once);
        }

        [Fact]
        public void AddUserIfNotExists_ReturnsAddedToDatabase_WhenSaved()
        {
            var user = CreateUserModel();
            var newUser = CreateUser();

            _mockMapper.Setup(r => r.Map<User>(user)).Returns(newUser);
            _mockUnitOfWork.Setup(r => r._userRepository.GetUserByObjectId(user.ObjectId)).Returns<IEnumerable<UserModel>>(null);
            _mockUnitOfWork.Setup(r => r._userRepository.AddUser(newUser)).Returns(true);
            _mockUnitOfWork.Setup(r => r.SaveChanges()).Returns(true);

            // Act
            var actual = _userService.AddUserIfNotExists(user);


            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.Equal("User added to the database", actual.Message);
            _mockUnitOfWork.Verify(r => r._userRepository.GetUserByObjectId(user.ObjectId), Times.Once);
            _mockUnitOfWork.Verify(r => r._userRepository.AddUser(newUser), Times.Once);
            _mockUnitOfWork.Verify(c => c.SaveChanges(), Times.Once);
            _mockMapper.Verify(r => r.Map<User>(user), Times.Once());


        }

        [Fact]
        public void AddUser_ReturnsSomethingWentWrong_WhenNotSaved()
        {
            var user = CreateUserModel();
            var newUser = CreateUser();

            _mockMapper.Setup(r => r.Map<User>(user)).Returns(newUser);
            _mockUnitOfWork.Setup(r => r._userRepository.GetUserByObjectId(user.ObjectId)).Returns<IEnumerable<UserModel>>(null);
            _mockUnitOfWork.Setup(r => r._userRepository.AddUser(newUser)).Returns(false);

            // Act
            var actual = _userService.AddUserIfNotExists(user);


            // Assert
            Assert.NotNull(actual);
            Assert.False(actual.Success);
            Assert.Equal("Something went wrong. Please try again later.", actual.Message);
            _mockUnitOfWork.Verify(r => r._userRepository.GetUserByObjectId(user.ObjectId), Times.Once);
            _mockUnitOfWork.Verify(r => r._userRepository.AddUser(newUser), Times.Once);
            _mockMapper.Verify(r => r.Map<User>(user), Times.Once());

        }       
        
        [Fact]
        public void AddUser_ReturnsAlreadySaved_WhenUserExistsInDatabase()
        {
            var user = CreateUserModel();
            var newUser = CreateUser();

            _mockUnitOfWork.Setup(r => r._userRepository.GetUserByObjectId(user.ObjectId)).Returns(newUser);

            // Act
            var actual = _userService.AddUserIfNotExists(user);


            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.Equal("User has already been registered to the database", actual.Message);
            _mockUnitOfWork.Verify(r => r._userRepository.GetUserByObjectId(user.ObjectId), Times.Once);

        }

        private UserModel CreateUserModel()
        {
            return _fixture.Build<UserModel>()
                .With(u => u.ObjectId, "1")    
                .Create();
        }
        private User CreateUser()
        {
            return _fixture.Build<User>()
                .With(u => u.ObjectId, "1") 
                .Without(u=> u.UserRoles)
                .Create();
        }

        public void Dispose()
        {
            _mockUnitOfWork.VerifyAll();
            _mockMapper.VerifyAll();
        }


    }
}
