namespace FinanceManagement.API.Features.Invoices.CreateInvoice
{
    public record CreateInvoiceRequest(Guid BookingId, Guid GuestId);
    public record CreateInvoiceResponse(Guid InvoiceId);
    public class CreateInvoiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/invoices", async (CreateInvoiceRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateInvoiceCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateInvoiceResponse>();

                return Results.Created($"/finance/invoices/{response.InvoiceId}", response);
            })
            .WithName("CreateInvoice")
            .Produces<CreateInvoiceResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Invoice")
            .WithDescription("Create Invoice");
        }
    }
}
