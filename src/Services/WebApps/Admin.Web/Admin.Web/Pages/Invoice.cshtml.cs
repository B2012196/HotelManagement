﻿namespace Admin.Web.Pages
{
    public class InvoiceModel(IFinanceService financeService, IHotelService hotelService, IBookingService bookingService, IGuestService guestService, 
        ILogger<InvoiceModel> logger) : PageModel
    {
        public IEnumerable<InvoiceView> InvoiceViewList { get; set; } = new List<InvoiceView>();
        public IEnumerable<InvoiceDetail> InvoiceDetailList { get; set; } = new List<InvoiceDetail>();
        public IEnumerable<Invoice> InvoiceList { get; set; } = new List<Invoice>();
        public IEnumerable<Service> ServiceList { get; set; } = new List<Service>();
        public IEnumerable<Booking> BookingList { get; set; } = new List<Booking>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public IEnumerable<RoomType> RoomTypeList { get; set; } = new List<RoomType>();
        public IEnumerable<BookingRoom> BookingRoomList { get; set; } = new List<BookingRoom>();
        public IEnumerable<Payment> PaymentList { get; set; } = new List<Payment>();
        public async Task<IActionResult> OnGetAsync(string SearchType, string SearchInput, string FilterStatus)
        {
            try
            {
                //get all invoice
                var resultGetInvoice = await financeService.GetInvoices();
                logger.LogInformation("Get all invoices");

                //get all invoicedetails
                var resultGetInvoiceDetail = await financeService.GetInvoiceDetails();
                logger.LogInformation("Get all invoicedetails");

                //get all services
                var resultGetService = await financeService.GetServices();
                logger.LogInformation("Get all services");

                //get all booking
                var resultbooking = await bookingService.GetBookings();
                logger.LogInformation("Get all bookings");

                //get all guest
                var resultguest = await guestService.GetGuests();
                logger.LogInformation("Get all guests");

                //get all bookingroom
                var resultbroom = await bookingService.GetBookingRooms();
                logger.LogInformation("Get all bookingroom");

                //get all room
                var resultroom = await hotelService.GetRooms();
                logger.LogInformation("Get all room");

                //get all payment
                var resultgetpayments = await financeService.GetPayments();
                logger.LogInformation("Get all payment");

                InvoiceList = resultGetInvoice.Invoices;
                InvoiceDetailList = resultGetInvoiceDetail.InvoiceDetails;
                ServiceList = resultGetService.Services;
                BookingList = resultbooking.Bookings;
                GuestList = resultguest.Guests;
                BookingRoomList = resultbroom.BookingRooms;
                RoomList = resultroom.Rooms;
                PaymentList = resultgetpayments.Payments;

                // Lọc danh sách Booking theo điều kiện tìm kiếm
                IEnumerable<Booking> filteredBookings = resultbooking.Bookings;

                if (!string.IsNullOrEmpty(SearchType) && !string.IsNullOrEmpty(SearchInput))
                {
                    switch (SearchType)
                    {
                        case "bookingId":
                            BookingList = BookingList.Where(b => b.BookingCode == SearchInput);
                            break;
                        case "guestName":
                            GuestList = GuestList.Where(b => b.FirstName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase));
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(FilterStatus))
                {
                    switch (FilterStatus)
                    {
                        case "pending":
                            InvoiceList = InvoiceList.Where(i => i.InvoiceStatus == InvoiceStatus.Pending);
                            break;
                        case "paid":
                            InvoiceList = InvoiceList.Where(i => i.InvoiceStatus == InvoiceStatus.Paid);
                            break;
                        case "partiallypaid":
                            InvoiceList = InvoiceList.Where(i => i.InvoiceStatus == InvoiceStatus.PartiallyPaid);
                            break;
                        default:
                            InvoiceList = InvoiceList.Where(i => i.InvoiceStatus == InvoiceStatus.Cancelled);
                            break;
                    }
                }

                var invoiceViews = new List<InvoiceView>();

                foreach (var invoice in InvoiceList)
                {
                    var booking = BookingList.SingleOrDefault(b => b.BookingId == invoice.BookingId);
                    if(booking != null)
                    {
                        var guest = GuestList.SingleOrDefault(g => g.GuestId == booking.GuestId);
                        if(guest != null)
                        {
                            var invoiceView = new InvoiceView
                            {
                                InvoiceId = invoice.InvoiceId,
                                BookingCode = booking.BookingCode,
                                GuestName = guest.LastName + " " +guest.FirstName,
                                InvoiceStatus = invoice.InvoiceStatus,
                                CheckinDate = booking.CheckinDate,
                                CheckoutDate = booking.CheckoutDate,
                                TotalBooking = booking.TotalPrice,
                                TotalPrice = invoice.TotalPrice,
                            };
                            var bookingrooms = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();

                            if (booking.BookingStatus != BookingStatus.Pending)
                            {
                                invoiceView.RoomNumber = "";
                                foreach (var bookroom in bookingrooms)
                                {
                                    var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookroom.RoomId);

                                    if (roomnumber != null)
                                    {
                                        invoiceView.RoomNumber += roomnumber.Number + " ";
                                    }
                                }
                            }

                            var details = InvoiceDetailList.Where(d => d.InvoiceId == invoice.InvoiceId).ToList();
                            var invoiceServiceViews = new List<InvoiceServiceView>();
                            invoiceView.TotalServiceUsed = 0;
                            foreach (var detail in details)
                            {
                                var invoiceServiceView = new InvoiceServiceView();

                                invoiceServiceView.ServiceNumber = detail.Numberofservice;
                                var service = ServiceList.SingleOrDefault(s => s.ServiceId == detail.ServiceId);
                                if (service != null)
                                {
                                    invoiceServiceView.ServiceName = service.ServiceName;
                                    invoiceServiceView.ServicePrice = service.ServicePrice;
                                }
                                invoiceServiceView.TotalServiceUsed = invoiceServiceView.ServicePrice * invoiceServiceView.ServiceNumber;
                                invoiceView.TotalServiceUsed += invoiceServiceView.TotalServiceUsed;
                                logger.LogWarning(invoiceView.TotalServiceUsed+""); 
                                invoiceServiceViews.Add(invoiceServiceView);    
                            }                                
                            invoiceView.InvoiceServiceViews = invoiceServiceViews;

                            var payment = PaymentList.SingleOrDefault(p => p.InvoiceId == invoice.InvoiceId);
                            if(payment != null)
                            {
                                invoiceView.PaymentTotal = payment.Amount;

                            }
                            invoiceView.RemainingAmount = invoiceView.TotalPrice - invoiceView.PaymentTotal;

                            invoiceViews.Add(invoiceView);
                        }
                    }
                }
                InvoiceViewList = invoiceViews;
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return Page();  
        }

        public async Task<IActionResult> OnPostPayInvoiceAsync(string InvoiceId, string GuestName)
        {
            try
            {
                Guid invoiceIdGuid;
                if (!Guid.TryParse(InvoiceId, out invoiceIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }
                Guid paymentMethodId = Guid.Parse("ed82b3a3-69ec-475e-961f-ba1a854d0348");
                var obj = new
                {
                    InvoiceId = invoiceIdGuid,
                    PaymentMethodId = paymentMethodId,
                    FullName = GuestName,
                };

                var resultCreatePayment = await financeService.CreatePayment(obj);
                return Redirect(resultCreatePayment.PaymentUrl);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Invoice");
        }

        public async Task<IActionResult> OnPostUpdateInvoiceAsync(Guid InvoiceId, decimal RemainingAmount)
        {
            try
            {
                var objUpdateInvoice = new
                {
                    InvoiceId = InvoiceId,
                };
                var resultUpdateInvoice = await financeService.UpdateInvoice(objUpdateInvoice);

                //create payment
                var objCreatePayment = new
                {
                    InvoiceId = InvoiceId,
                    PaymentMethodId = Guid.Parse("15d78f29-1977-41de-a41f-9270c89270c5"),
                    Amount = RemainingAmount
                };

                var resultCreatePayment = await financeService.CreatePaymentDirect(objCreatePayment);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Invoice");

        }

        public async Task<IActionResult> OnPostPrintInvoiceAsync()
        {
            // Cấp phép QuestPDF
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            // 1. Tạo hóa đơn PDF
            string fileName = $"Invoice_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine("wwwroot", "invoices", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            var invoice = new InvoiceDocument(
                "BOOK-123",
                DateTime.Now, // Ngày xuất hóa đơn
                "Lam Minh Duc",
                120000
            );

            invoice.GeneratePdf(filePath);

            // 3. Trả file PDF về trình duyệt
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", fileName);


        }

        private void HandleApiException(ApiException apiEx)
        {
            switch (apiEx.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                    break;

                case System.Net.HttpStatusCode.Forbidden:
                    Console.WriteLine("Forbidden: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                    break;

                default:
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                    break;
            }
        }
    }
}