using System.ComponentModel.DataAnnotations;

namespace Admin.Web.Models
{
    public enum InvoiceStatus
    {
        [Display(Name = "Đang chờ")]
        Pending = 1,         // Hóa đơn đang chờ xử lý
        [Display(Name = "Đã thanh toán")]
        Paid = 2,            // Hóa đơn đã thanh toán
        [Display(Name = "Bị hủy")]
        Cancelled = 3,       // Hóa đơn đã bị hủy
        [Display(Name = "Thanh toán online")]
        PartiallyPaid = 4,   // Hóa đơn đã thanh toán một phần
        [Display(Name = "Thất bại")]
        Failed = 5,          // Thanh toán hóa đơn thất bại
    }
}
