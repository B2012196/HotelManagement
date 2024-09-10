namespace BookingManagement.API.Features.Bookings.UpdateBookingCheckout
{
    public record UpdateBookingCheckoutCommand(Guid BookingId, DateTime CheckoutDate) 
        : ICommand<UpdateBookingCheckoutResult>;
    public record UpdateBookingCheckoutResult(bool IsSuccess);

    public class UpdateBookingCheckoutValidator : AbstractValidator<UpdateBookingCheckoutCommand>
    {
        public UpdateBookingCheckoutValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required.");
            RuleFor(x => x.CheckoutDate).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Check-out date cannot be in the past.");
        }
    }
    public class UpdateBookingCheckoutHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateBookingCheckoutCommand, UpdateBookingCheckoutResult>
    {
        public async Task<UpdateBookingCheckoutResult> Handle(UpdateBookingCheckoutCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);
            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }

            booking.CheckoutDate = command.CheckoutDate;
            if (booking.CheckinDate.HasValue && booking.CheckoutDate.HasValue)
            {
                booking.TotalPrice = CalculateTotalPrice(booking.CheckinDate.Value, booking.CheckoutDate.Value, 120000);
            }
            booking.BookingStatus = BookingStatus.CheckedOut;


            context.Bookings.Update(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateBookingCheckoutResult(true);
        }

        public decimal CalculateTotalPrice(DateTime checkinDate, DateTime checkoutDate, decimal roomPricePerDay)
        {
            // Quy định giờ chuẩn check-in và check-out là 12 giờ trưa
            TimeSpan standardCheckinTime = new TimeSpan(12, 0, 0);
            TimeSpan earlyCheckinTime1 = new TimeSpan(5, 0, 0);
            TimeSpan earlyCheckinTime2 = new TimeSpan(9, 0, 0);

            // Quy định các mốc giờ tính phí check-out trễ
            TimeSpan lateCheckoutTime1 = new TimeSpan(13, 0, 0);
            TimeSpan lateCheckoutTime2 = new TimeSpan(15, 0, 0);
            TimeSpan lateCheckoutTime3 = new TimeSpan(18, 0, 0);

            // Tính số giờ lưu trú
            TimeSpan duration = checkoutDate - checkinDate;
            int totalDays = (int)duration.TotalDays;

            // Kiểm tra trường hợp check-in sớm
            decimal earlyCheckinFee = 0;
            if (checkinDate.TimeOfDay >= earlyCheckinTime1 && checkinDate.TimeOfDay < earlyCheckinTime2)
            {
                earlyCheckinFee = roomPricePerDay * 0.5m; // Phí 50% giá phòng
            }
            else if (checkinDate.TimeOfDay >= earlyCheckinTime2 && checkinDate.TimeOfDay < standardCheckinTime)
            {
                earlyCheckinFee = roomPricePerDay * 0.3m; // Phí 30% giá phòng
            }

            // Tính tổng giá dựa trên số ngày
            decimal totalPrice = totalDays * roomPricePerDay + earlyCheckinFee;

            // Kiểm tra trường hợp trả phòng trễ
            decimal lateCheckoutFee = 0;
            if (checkoutDate.TimeOfDay > lateCheckoutTime1 && checkoutDate.TimeOfDay <= lateCheckoutTime2)
            {
                lateCheckoutFee = roomPricePerDay * 0.3m; // Phí 30% giá phòng
            }
            else if (checkoutDate.TimeOfDay > lateCheckoutTime2 && checkoutDate.TimeOfDay <= lateCheckoutTime3)
            {
                lateCheckoutFee = roomPricePerDay * 0.5m; // Phí 50% giá phòng
            }
            else if (checkoutDate.TimeOfDay > lateCheckoutTime3)
            {
                lateCheckoutFee = roomPricePerDay; // Phí 100% giá phòng
            }

            // Cộng phí trả phòng trễ (nếu có) vào tổng giá
            totalPrice += lateCheckoutFee;

            return totalPrice;

        }
    }
}
