using RSVP_tracker.Services;
using RSVPtracker.Core.Interfaces;
using RSVPtracker.Infrastructure.DIExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddRepositories(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// seed into db data
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var dbInit = service.GetRequiredService<IDatabaseInitialiser>();

    try
    {
        dbInit.SeedSampleData().Wait();
    } catch (Exception ex)
    {
        var msg = ex.ToString();
    }

}

app.Run();
