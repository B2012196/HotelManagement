namespace FinanceManagement.API.Features.Invoices.GetInvoices
{
    public record GetInvoicesRequest(int? pageNumber = 1, int? pageSize = 10, string? filterStatus = null);
    public record GetInvoicesResponse(IEnumerable<Invoice> Invoices, int TotalCount);
    public class GetInvoicesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/invoices", async([AsParameters] GetInvoicesRequest request, ISender sender) =>
            {
                InvoiceStatus? status = null;
                if (!string.IsNullOrEmpty(request.filterStatus))
                {
                    if (Enum.TryParse<InvoiceStatus>(request.filterStatus, true, out var parsedStatus))
                    {
                        status = parsedStatus;
                    }
                    else
                    {
                        return Results.BadRequest("Invalid filterStatus value.");
                    }
                }

                var result = await sender.Send(new GetInvoicesQuery(request.pageNumber, request.pageSize, status));
                Console.WriteLine("TotalCount: " + result.TotalCount);
                var response = result.Adapt<GetInvoicesResponse>();    

                Console.WriteLine("TotalCount: "+response.TotalCount);
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
