namespace FinanceManagement.API.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.InvoiceId)
                   .IsRequired();

            builder.Property(p => p.PaymentMethodId)
                   .IsRequired();

            builder.Property(p => p.CreateAt).HasColumnType("timestamp without time zone").IsRequired();

            builder.Property(p => p.Amount);

            builder.HasOne(p => p.Invoice)
                   .WithMany(o => o.Payments)
                   .HasForeignKey(p => p.InvoiceId);

            builder.HasOne(p => p.PaymentMethod)
                   .WithMany(pm => pm.Payments)
                   .HasForeignKey(p => p.PaymentMethodId);
        }
    }
}
