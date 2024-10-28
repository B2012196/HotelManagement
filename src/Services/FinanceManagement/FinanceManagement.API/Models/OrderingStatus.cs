namespace FinanceManagement.API.Models
{
    public enum OrderingStatus
    {
        Pending = 1,         // Hóa đơn đang chờ xử lý
        Paid = 2,            // Hóa đơn đã thanh toán
        Cancelled = 3,       // Hóa đơn đã bị hủy
        PartiallyPaid = 4,   // Hóa đơn đã thanh toán một phần
        Failed = 5,          // Thanh toán hóa đơn thất bại
    }
}
