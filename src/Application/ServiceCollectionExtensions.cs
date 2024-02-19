using Microsoft.Extensions.DependencyInjection;
using XSwift.Settings;
using Module.Contract;
using MediatR;
using Application;
using MongoDB.Driver;

namespace Module.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSetting databaseSetting,
            MongoDBSetting mongoDBSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null)
        {
            services.ConfigureDataStore(
                databaseSetting,
                mongoDBSetting,
                inMemoryDatabaseSetting);

            // MediatR Registrations
            services.AddMediatR(typeof(BookletService));
            services.AddMediatR(typeof(Persistence.BookletRepository.CreateNewBookletHandler));
            services.AddMediatR(typeof(Domain.BookletAggregation.CreateNewBooklet));

            // Application Services
            services.AddScoped<IBookletService, BookletService>();

            // Infrastructure Services
        }

        private static void ConfigureDataStore(
            this IServiceCollection services,
            DatabaseSetting databaseSetting,
            MongoDBSetting mongoDBSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null)
        {
            if (databaseSetting.IsInMemory)
            {
            }
            else
            {
                var client = new MongoClient(mongoDBSetting.ConnectionString);
                IMongoDatabase database = client.GetDatabase(mongoDBSetting.DatabaseName);
                services.AddSingleton(database);
            }

            new DatabaseInitialization(services).Initialize();
        }
    }
 }
