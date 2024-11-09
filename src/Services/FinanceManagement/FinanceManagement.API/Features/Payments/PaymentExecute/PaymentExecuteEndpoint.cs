using Microsoft.Extensions.Primitives;

namespace FinanceManagement.API.Features.Payments.PaymentExecute
{
    public record PaymentExecuteRequest(string VnpAmount, string VnpBankCode, string VnpBankTranNo, string VnpCardType, 
        string VnpOrderInfo, string VnpPayDate, string VnpResponseCode, string VnpTmnCode, string VnpTransactionNo, 
        string VnpTransactionStatus, string VnpTxnRef, string VnpSecureHash);
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

                // Ghi log các thuộc tính bắt đầu bằng "Vnp" trong PaymentExecuteRequest
                var properties = typeof(PaymentExecuteRequest).GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(request)?.ToString();
                    if (!string.IsNullOrEmpty(prop.Name) && prop.Name.StartsWith("Vnp") && value != null)
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
