
using HotelManagement.API.Models;

namespace HotelManagement.API.Features.RoomTypes.GetImage
{
    public record GetImageQuery(Guid TypeId) : IQuery<GetImageResult>;
    public record GetImageResult(byte[] ImageData, string ContentType);
    public class GetImageHandler(ApplicationDbContext context)
        : IQueryHandler<GetImageQuery, GetImageResult>
    {
        public async Task<GetImageResult> Handle(GetImageQuery query, CancellationToken cancellationToken)
        {
            var type = await context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == query.TypeId, cancellationToken);
            if(type == null)
            {
                throw new TypeNotFoundException(query.TypeId);
            }

            return new GetImageResult(type.Image, type.ImageContentType);
        }
    }
}
