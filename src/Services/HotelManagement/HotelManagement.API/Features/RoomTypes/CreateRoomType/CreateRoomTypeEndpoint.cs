
using HotelManagement.API.Features.Hotels.CreateHotel;

namespace HotelManagement.API.Features.RoomTypes.CreateRoomType
{
    public record CreateRoomTypeRequest(string Name, string Description, decimal PricePerNight, int Capacity);

    public record CreateRoomTypeResponse(Guid TypeId);
    public class CreateRoomTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/roomtypes", async (CreateRoomTypeRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRoomTypeCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRoomTypeResponse>();

                return Results.Created($"/hotels/{response.TypeId}", response);
            })
            .WithName("CreateRoomType")
            .Produces<CreateRoomTypeResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create RoomType")
            .WithDescription("Create RoomType");
        }
    }
}
