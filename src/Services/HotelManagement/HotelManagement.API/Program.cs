using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

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

var app = builder.Build();
//configure the HTTP request pipeline 
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", 
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
