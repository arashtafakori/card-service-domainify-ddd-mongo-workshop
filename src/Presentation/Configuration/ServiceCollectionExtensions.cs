using XSwift.Settings;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Module.Application;
using Application;
using Microsoft.Extensions.Options;

namespace Module.Presentation.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var mongoDBSetting = new MongoDBSetting();
            configuration.GetSection("MongoDBSetting").Bind(mongoDBSetting);

            services.ConfigureApplicationServices(
                databaseSetting: new DatabaseSetting(configuration),
                inMemoryDatabaseSetting: new InMemoryDatabaseSetting(configuration),
                mongoDBSetting: mongoDBSetting);
        }

        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            DatabaseSetting databaseSetting,
            MongoDBSetting mongoDBSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null)
        {
            //-- Application
            services.AddApplicationServices(
                databaseSetting, 
                mongoDBSetting,
                inMemoryDatabaseSetting);
        }

        public static void ConfigureLanguage(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.ConfigureLanguage(
                appLanguage: configuration.GetSection("AppLanguage").Value!);
        }

        public static void ConfigureLanguage(
            this IServiceCollection services,
            string appLanguage)
        {
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(appLanguage);
        }
    }
}
