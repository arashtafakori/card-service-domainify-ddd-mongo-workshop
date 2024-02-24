using MediatR;
using MongoDB.Driver;
using Module.Domain.BookletAggregation;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    internal class RetrieveBookletHandler :
        IRequestHandler<RetrieveBooklet, Booklet?>
    {
        private readonly IMongoDatabase _database;
        public RetrieveBookletHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Booklet?> Handle(
            RetrieveBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>
                .Filter.Eq(r => r.Id, request.Id);

            return (await collection.FindSync(filter)
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
