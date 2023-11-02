using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Data;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Repositories.AssignmentRepository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _dbContext;

        public AssignmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByProjectAsync(int projectId)
        {
            var assignments = await _dbContext.Assignments
                .Where(a => a.ProjectId == projectId)
                .ToListAsync();

            return assignments;
        }

        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(a => a.Id == id);

            return assignment;
        }

        public async Task<Assignment> AddAssignmentAsync(Assignment assignment)
        {
            var result = await _dbContext.Assignments.AddAsync(assignment);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            var dataBaseAssignment = await GetAssignmentByIdAsync(assignment.Id);

            if(dataBaseAssignment == null)
            {
                return;
            }

            dataBaseAssignment.Name = assignment.Name;
            dataBaseAssignment.Description = assignment.Description;
            dataBaseAssignment.Status = assignment.Status;
            dataBaseAssignment.ProjectId = assignment.ProjectId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignmentAsync(int id)
        {
            var dataBaseAssignment = await GetAssignmentByIdAsync(id);

            if(dataBaseAssignment == null)
            {
                return;
            }

            _dbContext.Remove(dataBaseAssignment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
