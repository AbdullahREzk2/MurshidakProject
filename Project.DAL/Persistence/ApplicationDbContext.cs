
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL.Persistence
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        //dbsets go here
        public DbSet<Level> Levels { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SubjectPrerequisite> SubjectPrerequisites { get; set; }
        public DbSet<StudentSubjectProgress> StudentSubjectProgress { get; set; }


    }
}
