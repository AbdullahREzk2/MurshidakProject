
namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class StudentSubjectProgressConfiguration: IEntityTypeConfiguration<StudentSubjectProgress>
    {
        public void Configure(EntityTypeBuilder<StudentSubjectProgress> builder)
        {
            builder.ToTable("StudentSubjectProgress");

            // -----------------------------
            // Primary Key
            // -----------------------------
            builder.HasKey(p => p.Id);

            // -----------------------------
            // Properties
            // -----------------------------
            builder.Property(p => p.IsCompleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(p => p.Grade)
                   .HasColumnType("decimal(5,2)");

            builder.Property(p => p.CompletedAt)
                   .IsRequired(false);

            // -----------------------------
            // Relationships
            // -----------------------------

            // Student (Identity User)
            builder.HasOne(p => p.Student)
                   .WithMany()
                   .HasForeignKey(p => p.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Subject
            builder.HasOne(p => p.Subject)
                   .WithMany()
                   .HasForeignKey(p => p.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------
            // Constraints / Indexes
            // -----------------------------

            // Prevent duplicate progress records
            builder.HasIndex(p => new { p.StudentId, p.SubjectId })
                   .IsUnique();
        }
    }
}
