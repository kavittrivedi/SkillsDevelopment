using EmployeeSkillsDevelopment.Infrastructure.Data;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeSkillsDevelopment.IntegrationTests
{

    public class UserRepositoryTests : IDisposable
    {
        private readonly IAppDbContext _appDbContext;
        private readonly List<User> _testUser;
        private readonly List<Role> _testRole;
        private readonly List<UserRole> _testUserRoles;
        public UserRepositoryTests()
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
            _testUser = SeedUser();
            _testRole = SeedRoles();
            _testUserRoles = SeedUserRoles();


        }
        [Fact]
        public void InsertUser_AddUserToDatabase()
        {
            // Arrange
            var userRepository = new UserRepository(_appDbContext);
            var newUser = new User
            {
                UserId=6,
                ObjectId = "123465",
                UserName = "user6",
                Email = "user6@gmail.com",
                IsDeleted = false
            };

            // Act
            bool result = userRepository.AddUser(newUser);
            var insertedUser = _appDbContext.Users.FirstOrDefault(c => c.UserName == "user6");

            // Assert
            Assert.True(result);
            Assert.NotNull(insertedUser);
            Assert.Equal("user6", insertedUser.UserName);
            Assert.Equal("user6@gmail.com", insertedUser.Email);
        }

        [Fact]
        public void InsertUserReturnsFalse_WhenUserIsNull()
        {
            // Arrange
            var userRepository = new UserRepository(_appDbContext);

            // Act
            bool result = userRepository.AddUser(null);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void GetUserByObjectId_FromDatabase()
        {
            // Arrange
          
            var userRepository = new UserRepository(_appDbContext);
            var seededuser = _appDbContext.Users.First();

            // Act
            var result = userRepository.GetUserByObjectId(seededuser.ObjectId);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(seededuser.UserName, result.UserName);
            Assert.Equal(seededuser.ObjectId, result.ObjectId);
            Assert.Equal(seededuser.IsDeleted, result.IsDeleted);
            Assert.Equal(seededuser.Email, result.Email);

        }
        [Fact]
        public void GetRoleByObjectId_FromDatabase()
        {
            // Arrange

            var userRepository = new UserRepository(_appDbContext);
            var seededuser = _appDbContext.Users.First();

            // Act
            var result = userRepository.GetRoleByObjectId(seededuser.ObjectId);


            // Assert
            Assert.NotNull(result);
           

        }

        public void Dispose()
        {
            // Cleanup test data
            _appDbContext.Database.ExecuteSqlRaw("DELETE FROM Users");
            _appDbContext.Database.ExecuteSqlRaw("DELETE FROM UserRoles");
            _appDbContext.Database.ExecuteSqlRaw("DELETE FROM Roles");
            _appDbContext.Dispose();
        }
        private List<UserRole> SeedUserRoles()
        {
            
            var userRole = new List<UserRole> 
            {
                new UserRole{UserRoleId=1,UserId=1,RoleId=1,IsActive=true},
                new UserRole{UserRoleId=2,UserId=2,RoleId=1,IsActive=true},
                new UserRole{UserRoleId=3,UserId=3,RoleId=1,IsActive=true},
                new UserRole{UserRoleId=4,UserId=4,RoleId=2,IsActive=true},
                new UserRole{UserRoleId=5,UserId=5,RoleId=2,IsActive=true}
            };
           
            _appDbContext.UserRoles.AddRange(userRole);
            _appDbContext.SaveChanges();

            return userRole;

        }
      private List<User> SeedUser()
        {
            var users = new List<User>
            {
                new User {UserId=1, ObjectId="123456",UserName="user1",Email="user1@gmail.com",IsDeleted=false},
                new User { UserId=2,ObjectId="123457",UserName="user2",Email="user2@gmail.com",IsDeleted=false},
                  new User {UserId=3, ObjectId="123458",UserName="user3",Email="user3@gmail.com",IsDeleted=false},
                    new User {UserId=4, ObjectId="123459",UserName="user4",Email="user4@gmail.com",IsDeleted=false},
                      new User { UserId=5,ObjectId="123450",UserName="user5",Email="user5@gmail.com",IsDeleted=false},
            };
           
            _appDbContext.Users.AddRange(users);
            _appDbContext.SaveChanges();

            return users;

        }
      private List<Role> SeedRoles()
        {
           
            var role = new List<Role>
            {
                new Role{ RoleId = 1 ,RoleName ="Role 1"},
                new Role{RoleId = 2 ,RoleName ="Role 2"}
            };
            _appDbContext.Roles.AddRange(role);
            _appDbContext.SaveChanges();

            return role;

        }
    
    }
}
