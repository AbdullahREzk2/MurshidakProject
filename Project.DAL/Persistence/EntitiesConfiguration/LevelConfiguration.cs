

namespace Project.DAL.Persistence.EntitiesConfiguration
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("Levels");


            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(l => l.Name).IsUnique();
        }


    }
}
