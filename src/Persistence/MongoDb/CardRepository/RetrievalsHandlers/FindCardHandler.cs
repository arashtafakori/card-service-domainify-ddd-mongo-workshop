using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
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
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);

            var filter = Builders<CardDocument>
                .Filter.Eq(r => r.Id, request.Id);

            if (request.EvenDeletedData == false)
                filter = filter & Builders<CardDocument>
                   .Filter.Eq(r => r.IsDeleted, request.EvenDeletedData);

            var findFluent = collection.Find(filter);

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
