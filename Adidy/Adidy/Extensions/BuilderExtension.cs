using Adidy.Contexts;
using Adidy.Log;
using Adidy.Log.Interface;
using Adidy.Services;
using Adidy.Services.Interface;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Adidy.Extensions
{
    public static class BuilderExtension
    {
        public static IServiceCollection ServiceInjection(this IServiceCollection services, IConfiguration configuration)
        {
            InjectServices(services);
            InjectBdd(services, configuration);
            InjectLog(services);
            LogManager.Setup().LoadConfigurationFromFile("./nlog.config");
            return services;
        }
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IMpandrayService, MpandrayService>();
            services.AddScoped<IUtilisateurService, UtilisateurService>();
            services.AddScoped<IPaiementAdidyService, PaiementAdidyService>();
            services.AddScoped<IPaiementIsantaonaService, PaiementIsantaonaService>();
        }


        public static void InjectBdd(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FiangonanaContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("Psql")!));
        }

        public static void InjectLog(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

    }
}
