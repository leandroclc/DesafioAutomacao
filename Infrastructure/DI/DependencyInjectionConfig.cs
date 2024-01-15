using Application.Repositories;
using Application.Services;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAluraService, AluraService>();
            services.AddScoped<IAluraRepository, AluraRepository>();
        }
    }
}
