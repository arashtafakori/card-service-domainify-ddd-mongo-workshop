using MediatR;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    public class GetCardHandler :
        IRequestHandler<GetCard, Card?>
    {
        private readonly IMongoDatabase _database;
        public GetCardHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Card?> Handle(
            GetCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);

            var filter = Builders<CardDocument>
                .Filter.Eq(b => b.Id, request.Id);

            if (request.IncludeDeleted == false)
                filter = filter & Builders<CardDocument>
                   .Filter.Eq(b => b.IsDeleted, request.IncludeDeleted);

            var findFluent = collection.Find(filter);

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
