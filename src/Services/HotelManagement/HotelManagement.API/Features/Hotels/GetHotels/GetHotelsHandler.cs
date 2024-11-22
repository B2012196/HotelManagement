namespace HotelManagement.API.Features.Hotels.GetHotels
{
    public record GetHotelsQuery(int? PageNumber = 1, int? PageSize = 10) 
        : IQuery<GetHotelsResult>;
        
    public record GetHotelsResult(IEnumerable<Hotel> Hotels);
    public class GetHotelsQueryHandler(ApplicationDbContext context)
        : IQueryHandler<GetHotelsQuery, GetHotelsResult>
    {
        public async Task<GetHotelsResult> Handle(GetHotelsQuery query, CancellationToken cancellationToken)
        {
            //Truy xuat du lieu tu database
            var hotels = context.Hotels.AsQueryable();

            // Thêm OrderBy để sắp xếp kết quả
            hotels = hotels.OrderBy(h => h.HotelId);

            //Ap dung phan trang neu co nhap PageNumber va PageSize
            if (query.PageNumber.HasValue && query.PageSize.HasValue)
            {
                int pageNumber = query.PageNumber.Value;    
                var pageSize = query.PageSize.Value;

                //Tinh so ban ghi can bo qua
                int skip = (pageNumber - 1) * pageSize;

                //Ap dung phan trang
                hotels = hotels.Skip(skip).Take(pageSize);

            }

            return new GetHotelsResult(await hotels.ToListAsync());
        }
    }
}
