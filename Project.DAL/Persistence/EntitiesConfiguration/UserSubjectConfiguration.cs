
namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class UserSubjectConfiguration : IEntityTypeConfiguration<UserSubject>
    {
        public void Configure(EntityTypeBuilder<UserSubject> builder)
        {


            builder.ToTable("UserSubjects");

            // Primary Key
            builder.HasKey(us => us.Id);

            // -----------------------------
            // Properties
            // -----------------------------
            builder.Property(us => us.RegisteredAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // -----------------------------
            // Relationships
            // -----------------------------
            builder.HasOne(us => us.User)
                   .WithMany(u => u.UserSubjects)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Subject)
                   .WithMany(s => s.UserSubjects)
                   .HasForeignKey(us => us.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------
            // Indexes
            // -----------------------------

            // منع الطالب من تسجيل نفس المادة مرتين
            builder.HasIndex(us => new { us.UserId, us.SubjectId })
                   .IsUnique();

            // Index لتحسين البحث حسب الطالب أو المادة
            builder.HasIndex(us => us.UserId);
            builder.HasIndex(us => us.SubjectId);
        }
    }
}
