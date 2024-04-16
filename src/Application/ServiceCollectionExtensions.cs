using Microsoft.Extensions.DependencyInjection;
using Contract;
using MediatR;
using MongoDB.Driver;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings,
            MongoDBSettings mongoDBSettings,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null)
        {
            services.ConfigureDataStore(
                databaseSettings,
                mongoDBSettings,
                inMemoryDatabaseSettings);

            // MediatR Registrations
            services.AddMediatR(typeof(BookletService));
            services.AddMediatR(typeof(Persistence.BookletStore.CreateBookletHandler));
            services.AddMediatR(typeof(Domain.BookletAggregation.CreateBooklet));

            // Application Services
            services.AddScoped<IBookletService, BookletService>();
            services.AddScoped<ICardService, CardService>();

            // Infrastructure Services
        }

        private static void ConfigureDataStore(
            this IServiceCollection services,
            DatabaseSettings databaseSettings,
            MongoDBSettings mongoDBSettings,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null)
        {
            if (databaseSettings.IsInMemory)
            {
            }
            else
            {
                var client = new MongoClient(mongoDBSettings.ConnectionString);
                IMongoDatabase database = client.GetDatabase(mongoDBSettings.DatabaseName);
                services.AddSingleton(database);
            }

            new DatabaseInitialization(services).Initialize();
        }
    }
 }
