namespace HotelManagement.API.Features.Hotels.DeleteHotel
{
    public record DeleteHotelResponse(bool IsSuccess);
    public class DeleteHotelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/hotels/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteHotelCommand(id));

                var response = result.Adapt<DeleteHotelResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteHotel")
            .Produces<DeleteHotelResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Hotel")
            .WithDescription("Delete Hotel");
        }
    }
}
