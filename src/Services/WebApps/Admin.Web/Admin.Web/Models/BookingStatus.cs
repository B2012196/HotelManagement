using System.ComponentModel.DataAnnotations;

namespace Admin.Web.Models
{
    public enum BookingStatus
    {
        [Display(Name = "Đang chờ")]
        Pending = 1,
        [Display(Name = "Đã xác nhận")]
        Confirmed = 2,
        [Display(Name = "Đã huỷ")]
        Canceled = 3,
        [Display(Name = "Đã nhận phòng")]
        CheckedIn = 4,
        [Display(Name = "Đã trả phòng")]
        CheckedOut = 5,

        None = 6,

    }
}
