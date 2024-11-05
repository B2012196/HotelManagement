namespace FinanceManagement.API.Features.Invoices.GetInvoices
{
    public record GetInvoicesResponse(IEnumerable<Invoice> Invoices);
    public class GetInvoicesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/invoices", async(ISender sender) =>
            {
                var result = await sender.Send(new GetInvoicesQuery());

                var response = result.Adapt<GetInvoicesResponse>();    

                return Results.Ok(response);
            })
            .WithName("GetInvoices")
            .Produces<GetInvoicesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Invoices")
            .WithDescription("Get Invoices");
        }
    }
}
