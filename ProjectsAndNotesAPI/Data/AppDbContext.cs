﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Assignments)
                .WithOne(a => a.Project)
                .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManager)
                .WithOne(pm => pm.Project)
                .HasForeignKey<ProjectManager>(pm => pm.ProjectId);
        }
    }
}
