using ProjectsAndNotesAPI.Enums;

namespace ProjectsAndNotesAPI.Models.DTO.Assignment
{
    public class AssignmentDtoWithoutProjectId
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AssignmentState Status { get; set; }

    }
}
