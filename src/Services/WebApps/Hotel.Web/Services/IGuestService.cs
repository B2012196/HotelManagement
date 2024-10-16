namespace Hotel.Web.Services
{
    public interface IGuestService
    {
        [Get("/guests/guests")]
        Task<GetGuestsResponse> GetGuests();
        [Get("/guests/guests/id/{GuestId}")]
        Task<GetGuestByIdResponse> GetGuestById(Guid GuestId);

        [Get("/guests/guests/userid/{UserId}")]
        Task<GetGuestByUserIdReponse> GetGuestByUserId(Guid UserId);

        [Post("/guests/guests")]
        Task<CreateGuestReponse> CreateGuest(Guest Guest);

        [Put("/guests/guests")]
        Task<UpdateGuestResponse> UpdateGuest(Guest Guest);

        [Delete("/guests/guests/{UserId}")]
        Task<DeleteGuestResponse> DeleteGuest(Guid UserId);
    }
}
