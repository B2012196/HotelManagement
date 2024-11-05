namespace FinanceManagement.API.Features.Invoices.DeleteInvoice
{
    public record DeleteInvoiceResponse(bool IsSuccess);
    public class DeleteInvoiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/finance/invoices/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteInvoiceCommand(id));

                var response = result.Adapt<DeleteInvoiceResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteInvoice")
            .Produces<DeleteInvoiceResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Invoice")
            .WithDescription("Delete Invoice");
        }
    }
}
