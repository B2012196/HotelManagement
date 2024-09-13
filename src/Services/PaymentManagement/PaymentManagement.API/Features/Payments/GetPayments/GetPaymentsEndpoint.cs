namespace PaymentManagement.API.Features.Payments.GetPayments
{
    public record GetPaymentsResponse(IEnumerable<Payment> Payments);
    public class GetPaymentsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/payments", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPaymentsQuery());

                var response = result.Adapt<GetPaymentsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetPayments")
            .Produces<GetPaymentsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Payments")
            .WithDescription("Get Payments");
        }
    }
}
