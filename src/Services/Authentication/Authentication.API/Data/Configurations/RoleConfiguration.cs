namespace Authentication.API.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);

            builder.Property(u => u.RoleName)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
