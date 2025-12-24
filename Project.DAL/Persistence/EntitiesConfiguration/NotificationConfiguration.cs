

namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Table name
            builder.ToTable("Notifications");

            // Primary Key
            builder.HasKey(n => n.Id);

            // -----------------------------
            // Properties
            // -----------------------------

            builder.Property(n => n.Message)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(n => n.IsRead)
                   .HasDefaultValue(false);

            builder.Property(n => n.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // -----------------------------
            // Relationships
            // -----------------------------

            // Notification → User
            builder.HasOne(n => n.User)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Subject)
                   .WithMany()
                   .HasForeignKey(n => n.SubjectId)
                   .OnDelete(DeleteBehavior.SetNull);

            // -----------------------------
            // Indexes
            // -----------------------------

            builder.HasIndex(n => n.UserId);
            builder.HasIndex(n => n.IsRead);
            builder.HasIndex(n => n.CreatedAt);
        }
    }
}
