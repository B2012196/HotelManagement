﻿namespace FinanceManagement.API.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(o => o.InvoiceId);

            builder.Property(o => o.BookingId)
                   .IsRequired();

            builder.Property(o => o.GuestId)
                   .IsRequired();

            builder.Property(o => o.CreateAt).IsRequired();

            builder.Property(o => o.InvoiceStatus).HasConversion<int>().IsRequired();

            builder.Property(o => o.TotalPrice);
        }
    }
}
