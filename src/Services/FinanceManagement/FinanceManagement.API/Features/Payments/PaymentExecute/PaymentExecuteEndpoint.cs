using Microsoft.Extensions.Primitives;

namespace FinanceManagement.API.Features.Payments.PaymentExecute
{
    public record PaymentExecuteRequest(string vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType, 
        string vnp_OrderInfo, string vnp_PayDate, string vnp_ResponseCode, string vnp_TmnCode, string vnp_TransactionNo, 
        string vnp_TransactionStatus, string vnp_TxnRef, string vnp_SecureHash);
    public record PaymentExecuteResponse(VnPaymentResponseModel VnPaymentResponseModel);
    public class PaymentExecuteEndpoint(IVnPayService vnPayService, ILogger<PaymentExecuteEndpoint> logger) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/vnpay", async (PaymentExecuteRequest request) =>
            {
                if (request == null)
                {
                    logger.LogWarning("Request là null");
                    return Results.Problem("Request là null.", statusCode: StatusCodes.Status500InternalServerError);
                }

                // Ghi log các thuộc tính bắt đầu bằng "vnp_" trong PaymentExecuteRequest
                var properties = typeof(PaymentExecuteRequest).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(request)?.ToString();
                    if (!string.IsNullOrEmpty(prop.Name) && prop.Name.StartsWith("vnp_") && value != null)
                    {
                        logger.LogWarning("Key: {Key}, Value: {Value}", prop.Name, value);
                    }
                }

                // Chuyển PaymentExecuteRequest thành QueryCollection
                var queryDictionary = properties.ToDictionary(
                    prop => prop.Name,
                    prop => new StringValues(prop.GetValue(request)?.ToString() ?? string.Empty)
                );
                var queryCollection = new QueryCollection(queryDictionary);
                foreach (var key in queryCollection.Keys)
                {
                    var value = queryCollection[key];
                    logger.LogInformation("queryCollection Key: {Key}, Value: {Value}", key, value);
                }
                var result = await vnPayService.PaymentExecute(queryCollection);

                var response = new PaymentExecuteResponse(result);

                return Results.Ok(response);
            })
            .WithName("GetVnPay")
            .Produces<PaymentExecuteResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get VnPay")
            .WithDescription("Get VnPay");
        }
    }

}
