using AutoMapper;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Infrastructure.Models;

namespace EmployeeSkillsDevelopment.Core.Mappers
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<UserRole, UserRoleModel>().ReverseMap();

        }

    }
}
