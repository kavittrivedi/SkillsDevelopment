using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Models
{
    public class StorageSettingsModel
    {
        public string Option { get; set; } = string.Empty;
        public string? BlobConnectionStrings { get; set; }

        public string? FileSystemPath { get; set; }
    }
}
