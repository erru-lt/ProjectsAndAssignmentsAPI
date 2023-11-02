using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.AssignmentRepository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAssignmentsByProjectAsync(int projectId);
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<Assignment> AddAssignmentAsync(Assignment assignment);
        Task UpdateAssignmentAsync(Assignment assignment);
        Task DeleteAssignmentAsync(int id);
    }
}