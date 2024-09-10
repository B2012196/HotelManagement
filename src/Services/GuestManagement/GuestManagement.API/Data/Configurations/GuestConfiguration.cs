namespace GuestManagement.API.Data.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(g => g.GuestId);
            builder.Property(g => g.FirstName).IsRequired().HasMaxLength(20);
            builder.Property(g => g.LastName ).IsRequired().HasMaxLength(20);
            builder.Property(g => g.DateofBirst).IsRequired();
            builder.Property(g => g.Address).IsRequired().HasMaxLength(200);
            builder.Property(g => g.Phone).IsRequired().HasMaxLength(15);
            builder.Property(g => g.Email).IsRequired().HasMaxLength(100);
        }
    }
}
