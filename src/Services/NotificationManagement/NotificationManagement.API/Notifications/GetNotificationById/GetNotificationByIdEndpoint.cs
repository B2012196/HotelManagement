namespace NotificationManagement.API.Notifications.GetNotificationById
{
    public record GetNotificationsByGuestIdResponse(IEnumerable<NotificationDto> NotificationDtos);
    public class GetNotificationByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/notifications/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetNotificationsByGuestIdQuery(id));

                var response = result.Adapt<GetNotificationsByGuestIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetNotificationsByGuestId")
            .Produces<GetNotificationsByGuestIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get NotificationsByGuestId")
            .WithDescription("Get NotificationsByGuestId");
        }
    }
}
