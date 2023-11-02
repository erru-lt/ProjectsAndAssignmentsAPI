using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Data;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects
                .Include(p => p.Assignments)
                .Include(p => p.ProjectManager)
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _dbContext.Projects
                .Include(p => p.Assignments)
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            var result = await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var dataBaseProject = await GetProjectByIdAsync(project.Id);

            if (dataBaseProject == null)
            {
                return;
            }

            dataBaseProject.Id = project.Id;
            dataBaseProject.Name = project.Name;
            dataBaseProject.Description = project.Description;
            dataBaseProject.Status = project.Status;
            dataBaseProject.StartDate = project.StartDate;
            dataBaseProject.EndDate = project.EndDate;

            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await GetProjectByIdAsync(id);

            if (project == null)
            {
                return;
            }

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}
