using ProjectsAndNotesAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAndNotesAPI.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "Incorrect assignment length")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Incorrect assignment length", MinimumLength = 20)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public AssignmentState Status { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
