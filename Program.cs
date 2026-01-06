using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// DbContext (Supabase / PostgreSQL)
builder.Services.AddDbContext<SugarDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// =======================
// 🔍 Test DB Connection (مؤقت)
// =======================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SugarDbContext>();
    try
    {
        db.Database.OpenConnection();
        Console.WriteLine("✅ Connected to Supabase successfully");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ DB Connection Failed");
        Console.WriteLine(ex.Message);
    }
}

// =======================
// Middleware
// =======================

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SugarBuddy API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

// لو HTTPS عامل مشكلة
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
