var builder = WebApplication.CreateBuilder(args);

// enable cross origin requests
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddSingleton<CosmosDbService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
