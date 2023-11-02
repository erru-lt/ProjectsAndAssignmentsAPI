using ProjectsAndNotesAPI.Enums;

namespace ProjectsAndNotesAPI.Models.DTO.Assignment
{
    public class AssignmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public AssignmentState Status { get; set; }
    }
}
