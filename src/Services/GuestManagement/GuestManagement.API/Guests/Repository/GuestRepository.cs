namespace GuestManagement.API.Guests.Repository
{
    public class GuestRepository(ApplicationDbContext context)
        : IGuestRepository
    {
        public async Task<Guid> CreateGuest(Guest Guest, CancellationToken cancellationToken)
        {
            context.Guests.Add(Guest);
            await context.SaveChangesAsync();
            return Guest.GuestId;
        }

        public async Task<bool> DeleteGuest(Guid GuestId, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == GuestId, cancellationToken);

            if (guest is null)
            {
                throw new GuestNotFoundException(GuestId);
            }

            context.Guests.Remove(guest);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Guest> GetGuestById(Guid GuestId, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == GuestId, cancellationToken);
            if(guest is null)
            {
                throw new GuestNotFoundException(GuestId);
            }

            return guest;
        }

        public async Task<IEnumerable<Guest>> GetGuests(CancellationToken cancellationToken)
        {
            var guests = await context.Guests.ToListAsync(cancellationToken);
            return guests;  

        }

        public async Task<bool> UpdateGuest(Guest Guest, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == Guest.GuestId, cancellationToken);
            if (guest is null)
            {
                throw new GuestNotFoundException(Guest.GuestId);
            }

            guest.UserId = Guest.UserId;
            guest.FirstName = Guest.FirstName;
            guest.LastName = Guest.LastName;
            guest.DateofBirst = Guest.DateofBirst;
            guest.Address = Guest.Address;

            //save database
            context.Guests.Update(guest);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
