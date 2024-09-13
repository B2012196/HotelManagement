﻿namespace PaymentManagement.API.Features.Payments.UpdatePayment
{
    public record UpdatePaymentRequest(Guid PaymentId, Guid PaymentMethodId);
    public record UpdatePaymentResponse(bool IsSuccess);
    public class UpdatePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/payments", async (UpdatePaymentRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdatePaymentCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdatePaymentResponse>();   

                return Results.Ok(response);
            })
            .WithName("UpdatePayment")
            .Produces<UpdatePaymentResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Payment")
            .WithDescription("Update Payment");
        }
    }
}
