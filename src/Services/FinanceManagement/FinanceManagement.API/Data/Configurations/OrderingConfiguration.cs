namespace FinanceManagement.API.Data.Configurations
{
    public class OrderingConfiguration : IEntityTypeConfiguration<Ordering>
    {
        public void Configure(EntityTypeBuilder<Ordering> builder)
        {
            builder.HasKey(o => o.OrderingId);

            builder.Property(o => o.BookingId)
                   .IsRequired();

            builder.Property(o => o.GuestId)
                   .IsRequired();

            builder.Property(o => o.CreateAt).HasColumnType("timestamp without time zone").IsRequired();

            builder.Property(o => o.OrderingStatus).HasConversion<int>().IsRequired();

            builder.Property(o => o.TotalPrice)
               .HasColumnType("decimal(12,2)");
        }
    }
}
