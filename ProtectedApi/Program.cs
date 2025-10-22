using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add JWT authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:5295";
        options.RequireHttpsMetadata = false;
        options.Audience = "api1";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Public endpoint (no authentication required)
app.MapGet("/", () => "Protected API is running!");

// Protected endpoint (requires valid token with 'api1' scope)
app.MapGet("/protected", [Authorize] () => new
{
    Message = "🎉 Success! You accessed a protected endpoint!",
    Timestamp = DateTime.UtcNow,
    Scope = "api1"
});

// Protected data endpoint
app.MapGet("/data", [Authorize] () => new
{
    Data = new[] { "Secret Data 1", "Secret Data 2", "Secret Data 3" },
    Message = "This data requires authentication!"
});

app.Run();
