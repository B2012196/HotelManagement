namespace BookingManagement.API.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.GuestId)
                   .IsRequired(); 
            
            builder.Property(b => b.BookingCode)
                   .IsRequired(false);

            builder.Property(b => b.TypeId)
                   .IsRequired();
            
            builder.Property(b => b.ExpectedCheckinDate).IsRequired();

            builder.Property(b => b.ExpectedCheckoutDate).IsRequired();

            builder.Property(b => b.CheckinDate);

            builder.Property(b => b.CheckoutDate);

            builder.Property(b => b.BookingStatus).HasConversion<int>().IsRequired();

            builder.Property(b => b.TotalPrice);

        }

    }
}
