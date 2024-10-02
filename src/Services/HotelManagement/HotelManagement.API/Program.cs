using BuildingBlocks.Messaging.MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

//Async communication service
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

//exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();

//health check
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// Thêm cấu hình JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Kiểm tra Issuer
        ValidateAudience = false, // Tắt kiểm tra Audience nếu không sử dụng
        ValidateLifetime = true, // Kiểm tra hạn token
        ValidateIssuerSigningKey = true, // Kiểm tra chữ ký của token

        ValidIssuer = "https://localhost:5056", // Issuer mà bạn đã dùng trong token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHotelwebsite14")), // Secret key giống với key mà bạn đã sử dụng để tạo token
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "hotelmanagementClient"));
});

var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", 
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//app.UseAuthentication();
app.UseAuthorization();
app.Run();
