using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartWash.Application.BookingSystem;
using SmartWash.Application.CookieSystem;
using SmartWash.Application.FeedbackSystem;
using SmartWash.Application.MachineSystem;
using SmartWash.Application.PaymentSystem;

namespace SmartWash.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            //services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IPaymentService, DemoPaymentService>();
            services.AddScoped<IMachineService, MachineService>();

            //services.AddScoped<ICookieService, CookieService>();

            return services;
        }
    }
}
