using AutoMapper;
using EmployeeSkillsDevelopment.Api.Configurations;
using EmployeeSkillsDevelopment.Api.DTOs;
using EmployeeSkillsDevelopment.Core.Models;
using EmployeeSkillsDevelopment.Infrastructure.Models;

namespace EmployeeSkillsDevelopment.Core.Mappers
{
    public class DtoMapperProfile: Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<EmployeeModel, EmployeeDto>().ReverseMap();
            CreateMap<UserModel, AddUserDto>().ReverseMap();
            CreateMap<StorageSettings, StorageSettingsModel>().ReverseMap();

        }

    }
}
