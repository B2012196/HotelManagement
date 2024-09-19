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

//Cấu hình MongoSettings từ appsetting.json
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

//Dang ki mongodb client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

//Dang ky mongodb 
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

//exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Đăng ký HealthChecks và thêm MongoDB Health Check
builder.Services.AddHealthChecks()
    .AddMongoDb(
        mongodbConnectionString: builder.Configuration["MongoSettings:ConnectionString"]!, // Chuỗi kết nối MongoDB
        name: "mongodb", // Tên health check
        timeout: TimeSpan.FromSeconds(3), // Thời gian chờ để kiểm tra kết nối
        tags: new[] { "db", "mongo" } // Các tag để lọc health check
    );

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // Sử dụng UIResponseWriter để định dạng phản hồi
});

app.Run();
