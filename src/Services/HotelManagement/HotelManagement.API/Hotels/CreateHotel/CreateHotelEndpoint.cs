namespace HotelManagement.API.Hotels.CreateHotel
{
    public record CreateHotelRequest
        (string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime);

    public record CreateHotelResponse(Guid Id);
    public class CreateHotelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/hotels", async (CreateHotelRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateHotelCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateHotelResponse>(); 

                return Results.Created($"/hotels/{response.Id}", response);    
            })
            .WithName("CreateHotel")
            .Produces<CreateHotelResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Hotel")
            .WithDescription("Create Hotel");
        }
    }
}
