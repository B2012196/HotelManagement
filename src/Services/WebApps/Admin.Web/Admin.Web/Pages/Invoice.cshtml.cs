using Admin.Web.Models;
using Microsoft.Extensions.Logging;

namespace Admin.Web.Pages
{
    public class InvoiceModel(IFinanceService financeService, IHotelService hotelService, IBookingService bookingService, IGuestService guestService, 
        ILogger<InvoiceModel> logger) : PageModel
    {
        public IEnumerable<InvoiceView> InvoiceViewList { get; set; } = new List<InvoiceView>();
        public IEnumerable<InvoiceDetail> InvoiceDetailList { get; set; } = new List<InvoiceDetail>();
        public IEnumerable<Service> ServiceList { get; set; } = new List<Service>();
        public IEnumerable<Booking> BookingList { get; set; } = new List<Booking>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public IEnumerable<RoomType> RoomTypeList { get; set; } = new List<RoomType>();
        public IEnumerable<BookingRoom> BookingRoomList { get; set; } = new List<BookingRoom>();
        public async Task<IActionResult> OnGetAsync()
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
                logger.LogInformation("bookingroom");

                //get all room
                var resultroom = await hotelService.GetRooms();
                logger.LogInformation("room");

                InvoiceDetailList = resultGetInvoiceDetail.InvoiceDetails;
                ServiceList = resultGetService.Services;
                BookingList = resultbooking.Bookings;
                GuestList = resultguest.Guests;
                BookingRoomList = resultbroom.BookingRooms;
                RoomList = resultroom.Rooms;

                var invoiceViews = new List<InvoiceView>();

                foreach (var invoice in resultGetInvoice.Invoices)
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
                                GuestName = guest.LastName + " " +guest.FirstName,
                                InvoiceStatus = invoice.InvoiceStatus,
                                CheckinDate = booking.CheckinDate,
                                CheckoutDate = booking.CheckoutDate,
                                TotalBooking = booking.TotalPrice,
                                TotalPrice = invoice.TotalPrice,
                            };
                            var bookingroom = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();

                            if (booking.BookingStatus != BookingStatus.Pending)
                            {
                                logger.LogWarning("Du lieu tu bookingroom: " +bookingroom[0].RoomId + "");
                                var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookingroom[0].RoomId);
                                if (roomnumber != null)
                                {
                                    logger.LogWarning("Du lieu tu room: " + roomnumber.Number);
                                    invoiceView.RoomNumber = roomnumber.Number;
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
                logger.LogError($"Error fetching guests: {ex.Message}");
            }
            return Page();  
        }

        public async Task<IActionResult> OnPostPayInvoiceAsync(string InvoiceId)
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
                    PaymentMethodId = paymentMethodId
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
                logger.LogError($"Error fetching guests: {ex.Message}");
            }
            return RedirectToPage("Invoice");
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
