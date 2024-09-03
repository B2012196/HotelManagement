using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Features.Hotels.GetHotels
{
    public record GetHotelsQuery() : IQuery<GetHotelsResult>;

    public record GetHotelsResult(IEnumerable<Hotel> Hotels);
    public class GetHotelsQueryHandler : IQueryHandler<GetHotelsQuery, GetHotelsResult>
    {
        private readonly ApplicationDbContext _context;

        public GetHotelsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetHotelsResult> Handle(GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var hotels = await _context.Hotels.ToListAsync(cancellationToken);
            return new GetHotelsResult(hotels);
        }
    }
}
