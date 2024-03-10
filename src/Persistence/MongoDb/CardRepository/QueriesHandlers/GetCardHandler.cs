using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
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
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);

            var filter = Builders<CardDocument>
                .Filter.Eq(b => b.Id, request.Id);

            if (request.EvenDeletedData == false)
                filter = filter & Builders<CardDocument>
                   .Filter.Eq(b => b.IsDeleted, request.EvenDeletedData);

            var findFluent = collection.Find(filter);

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
