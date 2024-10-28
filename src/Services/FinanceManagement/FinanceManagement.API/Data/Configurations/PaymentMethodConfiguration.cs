namespace FinanceManagement.API.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.PaymentMethodId);

            builder.Property(p => p.PaymentMethodName)
                   .IsRequired();
        }
    }
}
