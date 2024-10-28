namespace FinanceManagement.API.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.OrderingId)
                   .IsRequired();

            builder.Property(p => p.PaymentMethodId)
                   .IsRequired();

            builder.Property(p => p.CreateAt).HasColumnType("timestamp without time zone").IsRequired();

            builder.Property(p => p.Amount)
               .HasColumnType("decimal(12,2)");

            builder.HasOne(p => p.Ordering)
                   .WithMany(o => o.Payments)
                   .HasForeignKey(p => p.OrderingId);

            builder.HasOne(p => p.PaymentMethod)
                   .WithMany(pm => pm.Payments)
                   .HasForeignKey(p => p.PaymentMethodId);
        }
    }
}
