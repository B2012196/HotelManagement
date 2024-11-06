namespace FinanceManagement.API.Features.Invoices.GetInvoiceByBookingId
{
    public record GetInvoiceByBookingIdResponse(Invoice Invoice);
    public class GetInvoiceByBookingIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/invoices/bookingid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetInvoiceByBookingIdQuery(id));

                var response = result.Adapt<GetInvoiceByBookingIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetInvoiceByBookingId")
            .Produces<GetInvoiceByBookingIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Invoice By BookingId")
            .WithDescription("Get Invoice By BookingId");
        }
    }
}
