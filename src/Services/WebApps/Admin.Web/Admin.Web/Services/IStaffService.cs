namespace Admin.Web.Services
{
    public interface IStaffService
    {
        [Get("/staffs/staffs")]
        Task<GetStaffsResponse> GetStaffs();

        //[Get("/guests/guests/id/{GuestId}")]
        //Task<GetGuestByIdResponse> GetStaffById(Guid GuestId);

        [Post("/staffs/staffs")]
        Task<CreateStaffResponse> CreatetStaff(Guest Guest);

        [Put("/staffs/staffs")]
        Task<UpdateStaffResponse> UpdateStaff(Guest Guest);

        [Delete("/staffs/staffs/{GuestId}")]
        Task<DeleteStaffResponse> DeleteStaff(Guid GuestId);
    }
}
