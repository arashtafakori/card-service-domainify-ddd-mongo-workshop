using MediatR;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    internal class FindCardHandler :
        IRequestHandler<FindCard, Card?>
    {
        private readonly IMongoDatabase _database;
        public FindCardHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Card?> Handle(
            FindCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);

            var filter = Builders<CardDocument>
                .Filter.Eq(r => r.Id, request.Id);

            if (request.IncludeDeleted == false)
                filter = filter & Builders<CardDocument>
                   .Filter.Eq(r => r.IsDeleted, request.IncludeDeleted);

            var findFluent = collection.Find(filter);

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
