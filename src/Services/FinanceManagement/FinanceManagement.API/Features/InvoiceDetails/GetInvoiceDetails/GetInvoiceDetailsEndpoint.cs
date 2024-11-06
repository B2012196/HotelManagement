namespace FinanceManagement.API.Features.InvoiceDetails.GetInvoiceDetails
{
    public record GetInvoiceDetailsResponse(IEnumerable<InvoiceDetail> InvoiceDetails);
    public class GetInvoiceDetailsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/invoicedetails", async (ISender sender) =>
            {
                var result = await sender.Send(new GetInvoiceDetailsQuery());

                var response = result.Adapt<GetInvoiceDetailsResponse>();

                return Results.Ok(response); 
            })
            .WithName("GetInvoiceDetails")
            .Produces<GetInvoiceDetailsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Invoice Details")
            .WithDescription("Get Invoice Details");
        }
    }
}
