using AutoMapper;
using EmployeeSkillsDevelopment.Api.Configurations;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeSkillsDevelopment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "File")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly StorageSettings _storageSettings;
        public FileController(IFileService fileService,IMapper mapper,IOptions<StorageSettings> storageSettings)
        {
            _fileService = fileService;
            _mapper = mapper;
            _storageSettings = storageSettings.Value;

        }

        [HttpPost("Upload")]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            var storageSettings = _mapper.Map<StorageSettingsModel>(_storageSettings);
            var response = await _fileService.SaveFile(storageSettings, file);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
