using Hotel.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Hotel.Web.Pages
{
    public class PrivacyModel(IHotelService hotelService, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<HotelModel> HotelList { get; set; } = new List<HotelModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited");
            var result = await hotelService.GetHotels();
            HotelList = result.Hotels;
            return Page();
        }
    }

}
