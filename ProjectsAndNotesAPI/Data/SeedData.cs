using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Enums;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Data
{
    public class SeedData
    {
        public static async Task SeedAppData(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                if (dbContext.Projects.Any() == false)
                {
                    InsertProjects(dbContext);
                    await dbContext.SaveChangesAsync();
                }

                if(dbContext.ProjectManagers.Any() == false)
                {
                    InsertProjectManagers(dbContext);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Not empty");
                }
                
            }
        }

        private static void InsertProjects(AppDbContext dbContext)
        {
            dbContext.Projects.AddRange(new List<Project>()
            {
                new Project
    {
                    Name = "Проект 1",
                    Description = "Описание проекта 1",
                    Status = ProjectState.InProgress,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    ProjectManagerId = 1,
                },

                new Project
                {
                    Name = "Проект 2",
                    Description = "Описание проекта 2",
                    Status = ProjectState.Canceled,
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(2),
                    ProjectManagerId = 2,
                },

                new Project
                {
                    Name = "Проект 3",
                    Description = "Описание проекта 3",
                    Status = ProjectState.InProgress,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(6),
                    ProjectManagerId = 3,
                },

                new Project
                {
                    Name = "Проект 4",
                    Description = "Описание проекта 4",
                    Status = ProjectState.Pending,
                    StartDate = DateTime.Now.AddMonths(1),
                    EndDate = DateTime.Now.AddMonths(4),
                    ProjectManagerId = 4,
                },

                new Project
                {
                    Name = "Проект 5",
                    Description = "Описание проекта 5",
                    Status = ProjectState.Pending,
                    StartDate = DateTime.Now.AddMonths(2),
                    EndDate = DateTime.Now.AddMonths(3),
                    ProjectManagerId = 5,
                },

                new Project
                {
                    Name = "Проект 6",
                    Description = "Описание проекта 6",
                    Status = ProjectState.Canceled,
                    StartDate = DateTime.Now.AddMonths(1),
                    EndDate = DateTime.Now.AddMonths(5)
                },
            });
        }
        
        private static void InsertProjectManagers(AppDbContext dbContext)
        {
            dbContext.ProjectManagers.AddRange(new List<ProjectManager>
            {
                new ProjectManager
                {
                    Name = "Manager1",
                    Email = "Manager1@email.com",
                    ProjectId = 1,
                },
                new ProjectManager
                {
                    Name = "Manager2",
                    Email = "Manager2@email.com",
                    ProjectId = 2,
                },
                new ProjectManager
                {
                    Name = "Manager3",
                    Email = "Manager3@email.com",
                    ProjectId = 3,
                },
                new ProjectManager
                {
                    Name = "Manager4",
                    Email = "Manager4@email.com",
                    ProjectId = 4,
                },
                new ProjectManager
                {
                    Name = "Manager5",
                    Email = "Manager5@email.com",
                    ProjectId = 5,
                },
            });
        }
    }
}
