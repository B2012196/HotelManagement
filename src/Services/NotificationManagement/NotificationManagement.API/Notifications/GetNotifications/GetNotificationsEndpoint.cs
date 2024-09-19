namespace NotificationManagement.API.Notifications.GetNotifications
{
    public record GetNotificationsResponse(IEnumerable<NotificationDto> NotificationDtos);
    public class GetNotificationsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/notifications", async (ISender sender) =>
            {
                var result = await sender.Send(new GetNotificationsQuery());

                var response = result.Adapt<GetNotificationsResponse>();   
                
                return Results.Ok(response);
            })
            .WithName("GetNotifications")
            .Produces<GetNotificationsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Notifications")
            .WithDescription("Get Notifications");
        }
    }
}
