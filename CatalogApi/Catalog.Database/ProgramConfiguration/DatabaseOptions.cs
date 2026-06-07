using System.ComponentModel.DataAnnotations;

namespace Catalog.Database.ProgramConfiguration
{
    public class DatabaseOptions
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
