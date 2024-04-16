using MediatR;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    public class DeleteCardHandler :
        IRequestHandler<DeleteCard>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public DeleteCardHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            DeleteCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<CardDocument>.Filter.Eq(d => d.Id, preparedEntity.Id);
            
            var update = Builders<CardDocument>.Update.Set(d => d.IsDeleted, preparedEntity.IsDeleted);
            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
