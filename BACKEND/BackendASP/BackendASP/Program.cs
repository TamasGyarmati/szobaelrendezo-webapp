using BackendASP.Data;
using BackendASP.Models;
using BackendASP.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IRepositoryPLUS<Item>, ItemRepository>();
builder.Services.AddTransient<IRepositoryCRUD<Room>, RoomRepository>();
builder.Services.AddTransient<IRoomPlannerService, RoomPlannerService>();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// enableing CORS
app.UseCors(x => x
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")); // client side port

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Setting up default route
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}"
);

app.Run();