using Duende.IdentityServer.Validation;
using IdentityManagement.API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
//minimal API
builder.Services.AddCarter();

//validation
builder.Services.AddValidatorsFromAssembly(assembly);

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

//exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//health check
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

//// Cấu hình logging
//builder.Logging.ClearProviders();  // Xóa các provider mặc định
//builder.Logging.AddConsole();      // Thêm Console Logging
//builder.Logging.AddDebug();        // Thêm Debug Logging
//builder.Logging.SetMinimumLevel(LogLevel.Debug);  // Thiết lập mức độ log tối thiểu

//// Optional: Thiết lập mức độ log cho IdentityServer
//builder.Services.AddLogging(logging =>
//{
//    logging.AddFilter("Duende.IdentityServer", LogLevel.Debug);  // Chỉ log chi tiết cho IdentityServer
//});


//identity 
//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(Config.Clients) // Clients là những ứng dụng hoặc dịch vụ sẽ kết nối với IdentityServer để yêu cầu token
//    .AddInMemoryApiScopes(Config.ApiScopes) // là phạm vi mà token sẽ cấp quyền truy cập. Xác định các tài nguyên API client có thể truy cập sau khi được ủy quyền.
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    .AddTestUsers(Config.TestUsers)//dữ liệu liên quan đến danh tính người dùng mà IdentityServer có thể cung cấp
//    .AddDeveloperSigningCredential(); //IdentityServer sẽ tự động tạo ra một chứng chỉ ký tạm thời để ký các token (JWT).

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyPolicy", builder =>
//    {
//        builder.WithOrigins("https://localhost:5057") // Địa chỉ của Hotel.Web
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});
var app = builder.Build();
//configure the HTTP request pipeline 
//app.UseCors("MyPolicy");
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//app.UseIdentityServer();

app.UseAuthorization();

//app.MapDefaultControllerRoute();
// Map Razor Pages

app.Run();
