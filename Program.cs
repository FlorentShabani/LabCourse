using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Travista.Data;
using Travista.Models;
using Travista.Areas.Identity.Data;
using Travista.Controllers;
using System.Drawing.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DB Connection
builder.Services.AddDbContext<TravistaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TravistaContextConnectionString")));
builder.Services.AddDbContext<TravistaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TravistaContextConnection")));

builder.Services.AddDefaultIdentity<TravistaUser>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TravistaContext>();

builder.Services.AddScoped<UserManager<TravistaUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.MapRazorPages();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
