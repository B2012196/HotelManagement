namespace FinanceManagement.API.Features.InvoiceDetails.CreateInvoiceDetail
{
    public record CreateInvoiceDetailRequest(Guid InvoiceId, Guid ServiceId, int Numberofservice);
    public record CreateInvoiceDetailResponse(bool IsSuccess);
    public class CreateInvoiceDetailEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/invoicedetails", async (CreateInvoiceDetailRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateInvoiceDetailCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateInvoiceDetailResponse>();

                return Results.Ok(response);
            })
            .WithName("CreateInvoiceDetail")
            .Produces<CreateInvoiceDetailResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Invoice Detail")
            .WithDescription("Create Invoice Detail");
        }
    }
}
