using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<ResumeModel> CV { get; set; }
        public DbSet<EducationModel> Education { get; set; }
        public DbSet<FileModel> Files { get; set; }
    }
}