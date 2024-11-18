namespace FinanceManagement.API.Features.Invoices.UpdateInvoice
{
    public record UpdateInvoiceRequest(Guid InvoiceId);
    public record UpdateInvoiceResponse(bool IsSuccess);
    public class UpdateInvoiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/finance/invoices", async (UpdateInvoiceRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateInvoiceCommand>();

                var result = await sender.Send(command);    

                var response = result.Adapt<UpdateInvoiceResponse>();   

                return Results.Ok(response);
            })
            .WithName("UpdateInvoice")
            .Produces<UpdateInvoiceResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Invoice")
            .WithDescription("Update Invoice");
        }
    }
}
