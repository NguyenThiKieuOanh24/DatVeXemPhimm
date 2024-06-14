using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatVeXemPhimContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatVeXemPhimContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Thêm dịch vụ session
builder.Services.AddSession(options =>
{
    // Cấu hình tùy chỉnh session ở đây, ví dụ:
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DatVeXemPhimContext>();
        DbKhoiTao.khoiTao(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }

    //context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Sử dụng middleware session
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
