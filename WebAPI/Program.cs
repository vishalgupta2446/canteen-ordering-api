using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TastyByThreeMusketeers.Infrastructure.Data;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Applications.Services;
using TastyByThreeMusketeers.Core.Repositories;
var builder = WebApplication.CreateBuilder(args);
// 1️⃣ Add Controllers
builder.Services.AddControllers();
// 2️⃣ Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TastyByThreeMusketeers API", Version = "v1" });
});
// 3️⃣ Configure DbContext (SQLite)
builder.Services.AddDbContext<CanteenDbContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// 4️⃣ Dependency Injection (Repositories & Services)
// Repositories (Generic + Specific)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
// Services
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<NotificationService>();
// (Optional) Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
// 5️⃣ Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll"); // if using CORS
app.UseAuthorization();
app.MapControllers();
app.Run();