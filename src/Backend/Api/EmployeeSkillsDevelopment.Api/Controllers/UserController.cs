using AutoMapper;
using EmployeeSkillsDevelopment.Api.DTOs;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillsDevelopment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "User")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            
        }
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public IActionResult AddUserIfNotExists(AddUserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            var response = _userService.AddUserIfNotExists(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }  
        /// <summary>
        /// Fetch user-role
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet("GetUserRole")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        public IActionResult GetUserRole(string objectId)
        {
            var response = _userService.GetUserRole(objectId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}
