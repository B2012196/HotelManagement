
namespace StaffManagement.API.Data.Configurations
{
    public class StaffRoleConfiguration : IEntityTypeConfiguration<StaffRole>
    {
        public void Configure(EntityTypeBuilder<StaffRole> builder)
        {
            builder.HasKey(sr => sr.StaffRoleId);
            builder.Property(sr => sr.StaffRoleName).IsRequired().HasMaxLength(20);
        }
    }
}
