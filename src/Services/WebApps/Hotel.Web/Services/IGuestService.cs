namespace Hotel.Web.Services
{
    public interface IGuestService
    {
        [Get("/guests/guests")]
        Task<GetGuestsResponse> GetGuests();

        [Get("/guests/guests/userid/{UserId}")]
        Task<GetGuestByUserIdReponse> GetGuestByUserId(Guid UserId);

        [Post("/guests/guests")]
        Task<CreateGuestReponse> CreateGuest(GuestModel Guest);

        [Put("/guests/guests")]
        Task<UpdateGuestResponse> UpdateGuest(GuestModel Guest);

        [Delete("/guests/guests/{UserId}")]
        Task<DeleteGuestResponse> DeleteGuest(Guid UserId);
    }
}
