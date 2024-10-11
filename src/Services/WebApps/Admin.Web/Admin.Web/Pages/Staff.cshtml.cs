namespace Admin.Web.Pages
{
    public class StaffModel(IStaffService staffService) : PageModel
    {
        public IEnumerable<Staff> StaffList { get; set; } = new List<Staff>();
        public async Task<IActionResult> OnGetAsync()
        {

            var resultstaff = await staffService.GetStaffs();

            StaffList = resultstaff.Staffs;
            return Page();
        }
    }
}
