using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
// Cấu hình JWT Authentication

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5056"; // Địa chỉ của Authentication Service (Issuer)
        options.RequireHttpsMetadata = false; // Tùy vào môi trường, có thể bật hoặc tắt
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:5056", // Issuer khớp với token bạn phát hành
            ValidateAudience = false, // Tắt nếu không có Audience
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHotelwebsite14")),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddOcelot();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();

app.Run();
