
namespace Project.DAL.Configurations
{
    public class SubjectPrerequisiteConfiguration: IEntityTypeConfiguration<SubjectPrerequisite>
    {
        public void Configure(EntityTypeBuilder<SubjectPrerequisite> builder)
        {
            builder.ToTable("SubjectPrerequisites");

            // Composite PK
            builder.HasKey(sp => new { sp.SubjectId, sp.PrerequisiteId });

            // Subject → Prerequisites
            builder.HasOne(sp => sp.Subject)
                   .WithMany(s => s.Prerequisites)
                   .HasForeignKey(sp => sp.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prerequisite → IsPrerequisiteFor
            builder.HasOne(sp => sp.Prerequisite)
                   .WithMany(s => s.IsPrerequisiteFor)
                   .HasForeignKey(sp => sp.PrerequisiteId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
