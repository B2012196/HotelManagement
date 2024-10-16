﻿using BuildingBlocks.Messaging.Events;
using MassTransit.Transports;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace BookingManagement.API.Features.Bookings.Commands.UpdateBookingCheckout
{
    public record UpdateBookingCheckoutCommand(Guid BookingId)
        : ICommand<UpdateBookingCheckoutResult>;
    public record UpdateBookingCheckoutResult(bool IsSuccess);

    public class UpdateBookingCheckoutValidator : AbstractValidator<UpdateBookingCheckoutCommand>
    {
        public UpdateBookingCheckoutValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required.");
        }
    }
    public class UpdateBookingCheckoutHandler(ApplicationDbContext context, IHttpClientFactory httpClientFactory, ILogger<UpdateBookingCheckoutHandler> logger,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateBookingCheckoutCommand, UpdateBookingCheckoutResult>
    {
        public async Task<UpdateBookingCheckoutResult> Handle(UpdateBookingCheckoutCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);
            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }
            //update checkout
            booking.CheckoutDate = DateTime.Now;
            //request price and update statusbooking
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://hotelmanagement.api:8080/hotels/roomtypes/id/{booking.TypeId}");
            if (response.IsSuccessStatusCode)
            {
                var roomTypeResponse = await response.Content.ReadFromJsonAsync<RoomTypeResponseDTO>();
                var roomType = roomTypeResponse?.RoomType;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"Response JSON: {jsonResponse}");
                if (roomType != null)
                {
                    logger.LogInformation("start totalprice");
                    if (booking.CheckinDate.HasValue && booking.CheckoutDate.HasValue)
                    {
                        logger.LogInformation("Price"+roomType.pricePerNight);
                        booking.TotalPrice = CalculateTotalPrice(booking.CheckinDate.Value, booking.CheckoutDate.Value, roomType.pricePerNight);
                        logger.LogInformation("Price" + booking.TotalPrice);
                    }
                    booking.BookingStatus = BookingStatus.CheckedOut;

                    var bookingrooms = await context.BookingRooms.Where(r => r.BookingId == command.BookingId).ToListAsync(cancellationToken);
                    if (bookingrooms.Any())
                    {
                        var roomId = bookingrooms[0].RoomId;
                        var eventObj = new
                        {
                            BookingId = command.BookingId,
                            RoomId = roomId
                        };
                        //event BookingCheckoutEvent
                        var eventMessage = eventObj.Adapt<BookingCheckoutEvent>();
                        await publishEndpoint.Publish(eventMessage, cancellationToken);

                        context.Bookings.Update(booking);
                        await context.SaveChangesAsync(cancellationToken);

                        return new UpdateBookingCheckoutResult(true);
                    }
                }
            }
            return new UpdateBookingCheckoutResult(false);
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

            // Tính số ngày lưu trú
            int totalDays = (int)(checkoutDate.Date - checkinDate.Date).TotalDays + 1;

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
