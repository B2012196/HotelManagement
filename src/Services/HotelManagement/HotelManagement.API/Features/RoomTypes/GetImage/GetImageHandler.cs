namespace HotelManagement.API.Features.RoomTypes.GetImage
{
    public record GetImageQuery() : IQuery<GetImageResult>;
    public record GetImageResult(IEnumerable<Image> Images);
    public class GetImageHandler(ApplicationDbContext context)
        : IQueryHandler<GetImageQuery, GetImageResult>
    {
        public async Task<GetImageResult> Handle(GetImageQuery query, CancellationToken cancellationToken)
        {
            var image = await context.Images.ToListAsync(cancellationToken);

            return new GetImageResult(image);
        }
    }
}
