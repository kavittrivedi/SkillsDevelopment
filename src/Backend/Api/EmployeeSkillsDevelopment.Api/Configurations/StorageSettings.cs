namespace EmployeeSkillsDevelopment.Api.Configurations
{
    public class StorageSettings
    {
        public string Option { get; set; } = string.Empty;
        public string? BlobConnectionStrings { get; set; } 

        public string? FileSystemPath {  get; set; }
    }
}
