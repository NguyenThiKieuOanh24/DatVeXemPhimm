﻿using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DatVeXemPhimContext>();
    context.Database.EnsureCreated();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
