using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.HotelId);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Address).IsRequired().HasMaxLength(200);
            builder.Property(h => h.Phone).IsRequired().HasMaxLength(15);
            builder.Property(h => h.Email).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Stars).IsRequired();
            builder.Property(h => h.CheckinTime).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(h => h.CheckoutTime).HasColumnType("timestamp without time zone").IsRequired();
        }
    }
}
