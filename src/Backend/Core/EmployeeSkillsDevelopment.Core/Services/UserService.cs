using AutoMapper;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Models;

namespace EmployeeSkillsDevelopment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ServiceResponse<string> AddUserIfNotExists(UserModel user)
        {
            var response = new ServiceResponse<string>();
            var existingUser = _uow._userRepository.GetUserByObjectId(user.ObjectId);

            if (existingUser == null)
            {
                var newUser = _mapper.Map<User>(user);
                var result = _uow._userRepository.AddUser(newUser);
                
                if (result)
                {
                    _uow.SaveChanges();
                    response.Success = true;
                    response.Message = "User added to the database";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Something went wrong. Please try again later.";
                }
                return response;

            }
            else
            {
                response.Success = true;
                response.Message = "User has already been registered to the database";
                return response;
            }

        } 
        
        public ServiceResponse<string> GetUserRole(string objectId)
        {
            var response = new ServiceResponse<string>();
            var existingUser = _uow._userRepository.GetRoleByObjectId(objectId);
            if (existingUser != null)
            {
                response.Data = existingUser.Role.RoleName;
                response.Success = true;
                return response;

            }
            else
            {
                response.Success = false;
                response.Message = "User not found!";
                return response;
            }

        }
    }
}
