DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

var backendUrl = Environment.GetEnvironmentVariable("BACKEND_URL") ?? "http://localhost:5000";                                                                     
builder.WebHost.UseUrls(backendUrl);

var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:3000";
builder.Services.AddCors(options => {     
    options.AddDefaultPolicy(policy => policy.WithOrigins(frontendUrl)               
        .AllowAnyHeader().AllowAnyMethod());                                                 
});  

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    });
    app.UseCors();
}

app.UseHttpsRedirection();

app.MapGet("/api/ping", () => "pong")
    .WithName("Ping");

app.Run();