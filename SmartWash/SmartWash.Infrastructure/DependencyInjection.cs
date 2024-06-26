﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartWash.Domain.Interfaces;
using SmartWash.Infrastructure.Data;
using SmartWash.Infrastructure.Repositories;
using SmartWash.Infrastructure.Stripe;
using Stripe;

namespace SmartWash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //Stripe config
            StripeConfiguration.ApiKey = (config.GetSection("StripeOptions").GetSection("SecretKey").Value);
            services.AddScoped<IStripeAdapter, StripeAdapter>();

            //DataAccess
            //var cs = config.GetConnectionString("LocalServer");
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));

            var connectionString = config.GetConnectionString("LocalServer") ?? throw new InvalidOperationException("Connection string 'LocalServer' not found.");

            services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));


            //Repositories
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}
