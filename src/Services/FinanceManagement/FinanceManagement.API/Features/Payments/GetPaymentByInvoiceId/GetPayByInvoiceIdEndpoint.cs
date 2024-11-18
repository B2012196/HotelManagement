namespace FinanceManagement.API.Features.Payments.GetPaymentByInvoiceId
{
    public record GetPayByInvoiceIdResponse(Payment Payment);
    public class GetPayByInvoiceIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/payments/invoiceid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetPayByInvoiceIdQuery(id));

                var response = result.Adapt<GetPayByInvoiceIdResponse>();   

                return Results.Ok(response);
            })
            .WithName("GetPaymentByInvoiceId")
            .Produces<GetPayByInvoiceIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Payment By InvoiceId")
            .WithDescription("Get Payment By InvoiceId");
        }
    }
}
