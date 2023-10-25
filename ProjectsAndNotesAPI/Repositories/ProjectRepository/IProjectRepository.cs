using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.ProjectRepository
{
    public interface IProjectRepository
    {
        Task<Project> AddProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task UpdateProjectAsync(Project project);
    }
}
