namespace NotificationManagement.API.Notifications.CreateNotification
{
    public record CreateNotificationRequest
        (Guid GuestId, string Title, string Content);
    public record CreateNotificationResponse(string NotificationId);
    public class CreateNotificationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/notifications", async (CreateNotificationRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateNotificationCommand>();

                var result = await sender.Send(command);    

                var response = result.Adapt<CreateNotificationResponse>();

                return Results.Created($"/notifications/{response.NotificationId}", response);
            })
            .WithName("CreateNotification")
            .Produces<CreateNotificationResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Notification")
            .WithDescription("Create Notification");
        }
    }
}
