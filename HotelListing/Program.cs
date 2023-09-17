using AspNetCoreRateLimit;
using HotelListing;
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Adding database Context START,
// GetConnectionString is obtained from appsettings.json and then name of our connection string is "sqlConnection"
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
// Adding Database Context END

// Configure Identity Security Services START
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

// Configure Identity Security Services END

builder.Services.ConfigureVersioning();

// Configuration JWT START
builder.Services.ConfigureJWT(builder.Configuration);
// Configure JWT Stop


//Enabling CORS START
builder.Services.AddCors(p =>
{
    p.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});
// Enabling CORS END


builder.Services.AddAutoMapper(typeof(MapperInitializer));

//Setting up Serilog START
Log.Information("Application is starting");
try
{
    builder.Host.UseSerilog((hostContext, services, configuration) =>
    {
        configuration.WriteTo.File(
            path: "c:\\hotellistings\\logs\\log-.txt",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        );
    });
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
//Setting up Serilog END


builder.Services.AddControllers();

// Cache
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache();

// Rate Limiting and Throttling
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "HotelListing v1",

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureExceptionHandler();


app.UseHttpsRedirection();
app.UseCors("corsapp");

// For Cache
app.UseResponseCaching();
app.UseHttpCacheHeaders();

// For Throttling
app.UseIpRateLimiting();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
