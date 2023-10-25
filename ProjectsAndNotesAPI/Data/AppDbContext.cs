using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Data
{
    public class AppDbContext : DbContext   
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManager)
                .WithOne(pm => pm.Project)
                .HasForeignKey<ProjectManager>(pm => pm.ProjectId);
        }
    }
}
