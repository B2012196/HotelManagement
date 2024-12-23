﻿using System.Text.Json.Serialization;

namespace Admin.Web.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CreateAt { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class InvoiceView
    {
        public Guid InvoiceId { get; set; }
        public string BookingCode { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public decimal? TotalBooking { get; set; }
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public decimal RoomTypePrice { get; set; }
        public string RoomTypeName { get; set; }
        public List<InvoiceServiceView> InvoiceServiceViews { get; set; }
        public decimal? TotalServiceUsed { get; set; }
        public DateTime CreateAt { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? PaymentTotal { get; set; }
        public decimal? RemainingAmount { get; set; }
    }

    public class InvoiceServiceView
    {
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }

        [JsonPropertyName("serviceNumber")]
        public int ServiceNumber { get; set; }

        [JsonPropertyName("servicePrice")]
        public decimal ServicePrice { get; set; }

        [JsonPropertyName("totalServiceUsed")]
        public decimal TotalServiceUsed { get; set; }
    }

    public record GetInvoicesResponse(IEnumerable<Invoice> Invoices, int totalCount);
    public record GetInvoiceByBookingIdResponse(Invoice Invoice);
    public record CreateInvoiceResponse(Guid InvoiceId);
    public record UpdateInvoiceResponse(bool IsSuccess);


}
