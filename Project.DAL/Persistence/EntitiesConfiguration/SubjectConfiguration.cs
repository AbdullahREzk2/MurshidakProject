
namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");

            // -----------------------------
            // Primary Key
            // -----------------------------
            builder.HasKey(s => s.Id);

            // -----------------------------
            // Properties
            // -----------------------------
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Code)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(s => s.Time)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.IsAvailable)
                   .HasDefaultValue(true);

            // ❌ إزالة PrevReq لأنه غير عملي
            // builder.Property(s => s.PrevReq).HasDefaultValue(false);

            // -----------------------------
            // Relationships
            // -----------------------------

            // Subject → Doctor (ApplicationUser)
            builder.HasOne(s => s.Doctor)
                   .WithMany()
                   .HasForeignKey(s => s.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Subject → UserSubjects (Enrollment)
            builder.HasMany(s => s.UserSubjects)
                   .WithOne(us => us.Subject)
                   .HasForeignKey(us => us.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Subject → Prerequisites (Self-referencing Many-to-Many)
            builder.HasMany(s => s.Prerequisites)
                   .WithOne(sp => sp.Subject)
                   .HasForeignKey(sp => sp.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.IsPrerequisiteFor)
                   .WithOne(sp => sp.Prerequisite)
                   .HasForeignKey(sp => sp.PrerequisiteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // Indexes
            // -----------------------------
            builder.HasIndex(s => s.Code)
                   .IsUnique();
        }
    }
}
