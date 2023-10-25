using ProjectsAndNotesAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsAndNotesAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Incorrect project name length", MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public ProjectState Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
        public int ProjectManagerId { get; set; }
        public ProjectManager? ProjectManager { get; set; }
    }
}
