using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Enums;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Data
{
    public class SeedData
    {
        public static async Task EnsureRoles(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(await roleManager.RoleExistsAsync(roleName) == false)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public static async Task SeedAppData(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                //await dbContext.Database.MigrateAsync();

                if (dbContext.Projects.Any() == false)
                {
                    InsertProjects(dbContext);
                    await dbContext.SaveChangesAsync();
                }

                if (dbContext.ProjectManagers.Any() == false)
                {
                    InsertProjectManagers(dbContext);
                    await dbContext.SaveChangesAsync();
                }

                if (dbContext.Assignments.Any() == false)
                {
                    InsertAssignments(dbContext);
                    await dbContext.SaveChangesAsync();
                }
            }
        }


        private static void InsertProjects(AppDbContext dbContext)
        {
            dbContext.Projects.AddRange(new List<Project>()
            {
                new Project
    {
                    Name = "Project1",
                    Description = "Project1 description",
                    Status = ProjectState.InProgress,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    ProjectManagerId = 1,
                    Assignments = new List<Assignment>(),
                },
                new Project
                {
                    Name = "Project2",
                    Description = "Project2 description",
                    Status = ProjectState.Canceled,
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(2),
                    ProjectManagerId = 2,
                },

                new Project
                {
                    Name = "Project3",
                    Description = "Project3 description",
                    Status = ProjectState.InProgress,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(6),
                    ProjectManagerId = 3,
                },

                new Project
                {
                    Name = "Project4",
                    Description = "Project4 description",
                    Status = ProjectState.Pending,
                    StartDate = DateTime.Now.AddMonths(1),
                    EndDate = DateTime.Now.AddMonths(4),
                    ProjectManagerId = 4,
                },

                new Project
                {
                    Name = "Project5",
                    Description = "Project5 description",
                    Status = ProjectState.Pending,
                    StartDate = DateTime.Now.AddMonths(2),
                    EndDate = DateTime.Now.AddMonths(3),
                    ProjectManagerId = 5,
                },

                new Project
                {
                    Name = "Project6",
                    Description = "Project6 description",
                    Status = ProjectState.Canceled,
                    StartDate = DateTime.Now.AddMonths(1),
                    EndDate = DateTime.Now.AddMonths(5),
                    ProjectManagerId = 2,
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
        private static void InsertAssignments(AppDbContext dbContext)
        {
            dbContext.Assignments.AddRange(new List<Assignment>()
            {
                new Assignment
                {
                    Name = "agnmnt1",
                    Description = "First assignment of project 1",
                    Status = AssignmentState.Pending,
                    ProjectId= 1,
                },
                new Assignment
                {
                    Name = "agnmnt2",
                    Description = "Second assignment of project 1",
                    Status = AssignmentState.InProcess,
                    ProjectId= 1,
                },
                new Assignment
                {
                    Name = "agnmnt3",
                    Description = "Third assignment of project 1",
                    Status = AssignmentState.InProcess,
                    ProjectId= 1,
                },
                new Assignment 
                {
                    Name = "agnmnt1",
                    Description = "First assignment of project 2",
                    Status = AssignmentState.Canceled,
                    ProjectId= 2 ,
                },
            });
        }
    }
}
