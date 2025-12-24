
namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.ToTable("AspNetUsers");

            builder
             .OwnsMany(u => u.RefreshTokens)
             .ToTable("RefreshTokens")
             .WithOwner()
             .HasForeignKey("UserId");

            builder.Property(u => u.UniId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(u => u.UniId).IsUnique();

            builder
            .Property(x => x.firstName)
            .HasMaxLength(100);

            builder
                .Property(x => x.lastName)
                .HasMaxLength(100);


            builder.HasOne(u => u.Specialization)
                   .WithMany(s => s.Users)
                   .HasForeignKey(u => u.SpecializationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Level)
                   .WithMany(l => l.Users)
                   .HasForeignKey(u => u.LevelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserSubjects)
                   .WithOne(us => us.User)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                   .WithOne(n => n.User)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }

}
