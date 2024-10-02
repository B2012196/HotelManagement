namespace GuestManagement.API.Guests.Repository
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetGuests(CancellationToken cancellationToken);
        Task<Guest> GetGuestById(Guid GuestId, CancellationToken cancellationToken);
        Task<Guid> CreateGuest(Guest Guest, CancellationToken cancellationToken);
        Task<bool> UpdateGuest(Guest Guest, CancellationToken cancellationToken);
        Task<bool> DeleteGuest(Guid GuestId, CancellationToken cancellationToken);
    }
}
