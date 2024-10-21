using StaffManagement.API.Features.Staffs.Repositories;

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

builder.Services.AddScoped<IStaffRepository, StaffRepository>();

//health check
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
