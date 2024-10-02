﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Hotel.Web.Services;
using Microsoft.Extensions.Options;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IHotelService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    });

builder.Services.AddRefitClient<IAuthentication>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    });
// Thêm session vào container dịch vụ
builder.Services.AddDistributedMemoryCache(); // Cần thiết cho Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session
    options.Cookie.HttpOnly = true; // Đảm bảo cookie session chỉ được truy cập qua HTTP
    options.Cookie.IsEssential = true; // Cần thiết nếu sử dụng cookie yêu cầu sự đồng ý của người dùng
});

builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseAuthentication();
//app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();