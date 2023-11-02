using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.ProjectManagerRepository
{
    public interface IProjectManagerRepository
    {
        Task<ProjectManager> AddProjectManagerAsync(ProjectManager projectManager);
        Task DeleteProjectManagerAsync(int id);
        Task<IEnumerable<ProjectManager>> GetAllProjectManagersAsync();
        Task<ProjectManager> GetProjectManagerByIdAsync(int id);
        Task UpdateProjectManagerAsync(int id, ProjectManager projectManager);
    }
}