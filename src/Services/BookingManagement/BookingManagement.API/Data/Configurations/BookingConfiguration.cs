namespace BookingManagement.API.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.GuestId)
                   .IsRequired();
            builder.Property(b => b.TypeId)
                   .IsRequired();
            
            builder.Property(b => b.ExpectedCheckinDate).HasColumnType("timestamp without time zone").IsRequired();

            builder.Property(b => b.ExpectedCheckoutDate).HasColumnType("timestamp without time zone").IsRequired();

            builder.Property(b => b.CheckinDate).HasColumnType("timestamp without time zone");

            builder.Property(b => b.CheckoutDate).HasColumnType("timestamp without time zone");

            builder.Property(b => b.BookingStatus).HasConversion<int>().IsRequired();

            builder.Property(b => b.TotalPrice)
               .HasColumnType("decimal(12,2)");

        }

    }
}
