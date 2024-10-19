
using AutoFixture;
using AutoMapper;
using EmployeeSkillsDevelopment.Api.Controllers;
using EmployeeSkillsDevelopment.Api.DTOs;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeSkillsDevelopment.Tests.Api
{
    public class UserControllerTests : IDisposable
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mapper;
        private readonly UserController _controller;
        private readonly IFixture _fixture;


        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();
            _fixture = new Fixture();
            _controller = new UserController(_mockUserService.Object,_mapper.Object);

        }

        [Fact]
        [Trait("User", "UserControllerTests")]
        public void AddUserIfNotExists_ReturnsOk()
        {
            // Arrange
            var response = new ServiceResponse<string>
            {
                Success = true,
            };
            var userDto =  _fixture.Create<AddUserDto>();
            var userModel =  _fixture.Create<UserModel>();

            _mapper.Setup(s => s.Map<UserModel>(userDto)).Returns(userModel);
            _mockUserService.Setup(s => s.AddUserIfNotExists(userModel)).Returns(response);

            // Act
            var actual = _controller.AddUserIfNotExists(userDto) as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockUserService.Verify(s => s.AddUserIfNotExists(userModel), Times.Once);
            _mapper.Verify(s => s.Map<UserModel>(userDto), Times.Once());

        }

        [Fact]
        [Trait("User", "UserControllerTests")]
        public void AddUserIfNotExists_ReturnsBadRequest()
        {
            // Arrange
            var response = new ServiceResponse<string>
            {
                Success = false,
                Data = null
            };
            var userDto =  _fixture.Create<AddUserDto>();
            var userModel =  _fixture.Create<UserModel>();

            _mapper.Setup(s => s.Map<UserModel>(userDto)).Returns(userModel);
            _mockUserService.Setup(s => s.AddUserIfNotExists(userModel)).Returns(response);

            // Act
            var actual = _controller.AddUserIfNotExists(userDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockUserService.Verify(s => s.AddUserIfNotExists(userModel), Times.Once);
        }

        [Fact]
        [Trait("User", "UserControllerTests")]
        public void GetUserRole_ReturnsOk()
        {
            string objectId = "1";
            var response = new ServiceResponse<string>
            {
                Success = true,
                Data = "Admin"
            };
            _mockUserService.Setup(s => s.GetUserRole(objectId)).Returns(response);

            // Act
            var actual = _controller.GetUserRole(objectId) as OkObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockUserService.Verify(s => s.GetUserRole(objectId), Times.Once);


        }        
        
        [Fact]
        [Trait("User", "UserControllerTests")]
        public void GetUserRole_ReturnsBadRequest()
        {
            string objectId = "1";
            var response = new ServiceResponse<string>
            {
                Success = false,
            };
            _mockUserService.Setup(s => s.GetUserRole(objectId)).Returns(response);

            // Act
            var actual = _controller.GetUserRole(objectId) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(400, actual.StatusCode);
            Assert.Equal(response, actual.Value);
            _mockUserService.Verify(s => s.GetUserRole(objectId), Times.Once);


        }


        public void Dispose()
        {
            _mockUserService.VerifyAll();
            _mapper.VerifyAll();
        }


    }
}
