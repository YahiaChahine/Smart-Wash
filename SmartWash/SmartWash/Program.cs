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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddDomain();

//DataAccess
var cs = builder.Configuration.GetConnectionString("LocalServer");
builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(cs));
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));

//Stripe config
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeOptions:SecretKey");
builder.Services.AddScoped<IStripeAdapter, StripeAdapter>();
//include addscoped for all repositories i built ex FeedbackRepository and IFeedbackRepository .. etc
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();
builder.Services.AddScoped<IReplyRepository, ReplyRepository>();


//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
