using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Data;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.ProjectManagerRepository
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectManagerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProjectManager>> GetAllProjectManagersAsync()
        {
            return await _dbContext.ProjectManagers.ToListAsync();
        }

        public async Task<ProjectManager> GetProjectManagerByIdAsync(int id)
        {
            return await _dbContext.ProjectManagers.FirstOrDefaultAsync(pm => pm.Id == id);
        }

        public async Task<ProjectManager> AddProjectManagerAsync(ProjectManager projectManager)
        {
            var result = await _dbContext.AddAsync(projectManager);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateProjectManagerAsync(int id, ProjectManager projectManager)
        {
            var dataBaseProjectManager = await GetProjectManagerByIdAsync(id);

            if (dataBaseProjectManager == null)
            {
                return;
            }

            dataBaseProjectManager.Name = projectManager.Name;
            dataBaseProjectManager.Email = projectManager.Email;
            dataBaseProjectManager.ProjectId = projectManager.ProjectId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectManagerAsync(int id)
        {
            var dataBaseProjectManager = await GetProjectManagerByIdAsync(id);

            if (dataBaseProjectManager == null)
            {
                return;
            }

            _dbContext.Remove(dataBaseProjectManager);
            await _dbContext.SaveChangesAsync();
        }
    }
}
