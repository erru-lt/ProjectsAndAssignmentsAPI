using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectsAndNotesAPI.Models
{
    public class ProjectManager
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Incorrect name length", MinimumLength = 4)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
    }
}
