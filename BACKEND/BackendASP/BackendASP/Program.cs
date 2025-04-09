using BackendASP.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS engedélyezése
app.UseCors(x => x
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")); // kliensoldali port

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