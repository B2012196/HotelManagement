using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.InteropServices.JavaScript;

namespace HotelManagement.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.HotelID);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Address).IsRequired().HasMaxLength(200);
            builder.Property(h => h.Phone).IsRequired().HasMaxLength(15);
            builder.Property(h => h.Email).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Stars).IsRequired();
            builder.Property(h => h.CheckinTime).IsRequired();
            builder.Property(h => h.CheckoutTime).IsRequired();
        }
    }
}
