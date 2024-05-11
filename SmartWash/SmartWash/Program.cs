using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SmartWash.Application;
using SmartWash.Domain;
using SmartWash.Infrastructure;
using SmartWash.Infrastructure.Repositories;
using SmartWash.Infrastructure.Data;
using SmartWash.Domain.Entities;
using Stripe;
using SmartWash.Infrastructure.Stripe;
using SmartWash.Domain.Interfaces;
using SmartWash.WebUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddDomain();

builder.Services.AddScoped<StateContainer>(); // Stores the state of the application

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<SignInManager<ApplicationUser>>();

builder.Services.AddIdentityCore<Admin>().AddEntityFrameworkStores<DataContext>();
builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbcontext = services.GetRequiredService<DataContext>();
        DataInitializer.Initialize(dbcontext);
    }
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
