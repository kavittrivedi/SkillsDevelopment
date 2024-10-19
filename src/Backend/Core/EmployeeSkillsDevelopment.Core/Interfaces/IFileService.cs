using EmployeeSkillsDevelopment.Core.Models;
using Microsoft.AspNetCore.Http;


namespace EmployeeSkillsDevelopment.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFile(StorageSettingsModel settings, IFormFile file);
        
    }
}
