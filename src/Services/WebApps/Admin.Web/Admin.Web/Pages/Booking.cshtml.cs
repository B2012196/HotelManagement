namespace Admin.Web.Pages
{
    public class BookingModel(IBookingService bookingService, IGuestService guestService, IHotelService hotelService, ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultbooking = await bookingService.GetBookings();
                var bookingViewList = new List<BookingView>();
                // Tạo danh sách các task cho các API call
                var guestTasks = resultbooking.Bookings.Select(b => guestService.GetGuestById(b.GuestId)).ToList();
                var typeTasks = resultbooking.Bookings.Select(b => hotelService.GetRoomTypeById(b.TypeId)).ToList();
                // Thực hiện tất cả các task đồng thời
                var guests = await Task.WhenAll(guestTasks);
                var types = await Task.WhenAll(typeTasks);
                var bookingsList = resultbooking.Bookings.ToList();
                for (int i = 0; i < resultbooking.Bookings.Count(); i++)
                {
                    //var guest = await guestService.GetGuestById(booking.GuestId);
                    //var type = await hotelService.GetRoomTypeById(booking.TypeId);

                    var booking = bookingsList[i];
                    var guest = guests[i];
                    var type = types[i];
                    if (guest == null || guest.Guest == null || type == null || type.RoomType == null)
                    {
                        logger.LogWarning($"Missing data for booking {booking.BookingId}");
                        continue; // Skip this iteration if any necessary data is missing
                    }

                    var bookingView = new BookingView
                    {
                        BookingId = booking.BookingId,
                        TypeId = booking.TypeId,
                        TypeName = type.RoomType.Name,
                        GuestFirstName = guest.Guest.FirstName,
                        GuestLastName = guest.Guest.LastName,
                        ExpectedCheckinDate = booking.ExpectedCheckinDate,
                        ExpectedCheckoutDate = booking.ExpectedCheckoutDate,
                        CheckinDate = booking.CheckinDate,
                        CheckoutDate = booking.CheckoutDate,
                        RoomQuantity = booking.RoomQuantity,
                        BookingStatus = booking.BookingStatus,
                        TotalPrice = booking.TotalPrice,
                    };
                    bookingViewList.Add(bookingView);
                }
                BookingList = bookingViewList;

                var resultroom = await hotelService.GetRooms();
                RoomList = resultroom.Rooms;

            }
            catch (Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmBookingAsync(string BookingId, string RoomId)
        {
            Guid bookingIdGuid;
            Guid roomIdGuid;
            if (!Guid.TryParse(BookingId, out bookingIdGuid) || !Guid.TryParse(RoomId, out roomIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var confirm = new
            {
                BookingId = bookingIdGuid,
                RoomId = roomIdGuid
            };

            var resultconfirm = await bookingService.UpdateBookingConfirm(confirm);

            if(!resultconfirm.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update confirm the booking");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckinBookingAsync(string BookingId)
        {
            Guid bookingIdGuid;
            if (!Guid.TryParse(BookingId, out bookingIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var checkin = new
            {
                BookingId = bookingIdGuid
            };

            var resultconfirm = await bookingService.UpdateBookingCheckin(checkin);
            if (!resultconfirm.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update checkin the booking");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckoutBookingAsync(string BookingId)
        {
            Guid bookingIdGuid;
            if (!Guid.TryParse(BookingId, out bookingIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var checkin = new
            {
                BookingId = bookingIdGuid
            };

            var resultconfirm = await bookingService.UpdateBookingCheckout(checkin);
            if (!resultconfirm.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update checkout the booking");
            }

            return RedirectToPage("Booking");
        }

    }
}
