using Authentication.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

//http
builder.Services.AddHttpClient();

//Async communication service
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

//exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//health check
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
//add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Địa chỉ của Authentication Service (Issuer)
        options.Authority = "https://localhost:5056";  // Đặt đây là URL của IdentityServer hoặc Auth Service của bạn

        // Nếu đang phát triển với HTTP (chưa có SSL), tắt kiểm tra HTTPS
        options.RequireHttpsMetadata = false;  // Cảnh báo, chỉ nên tắt ở môi trường phát triển

        // Cấu hình xác thực JWT token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // Kiểm tra Issuer
            ValidIssuer = "https://localhost:5056",  // Đây phải là URL của Identity Server hoặc Token Issuer

            ValidateAudience = false,  // Tắt nếu không kiểm tra Audience trong token
            ValidateLifetime = true,  // Kiểm tra thời gian hết hạn của token

            // Key xác thực token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHotelwebsite14")),
            ValidateIssuerSigningKey = true // Xác nhận key ký token
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
    options.AddPolicy("RequireGuestRole", policy => policy.RequireRole("Guest"));
});

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseAuthentication();
app.UseAuthorization();
app.Run();
