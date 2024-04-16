using MediatR;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    public class EditCardHandler :
        IRequestHandler<EditCard>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EditCardHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<Unit> Handle(
            EditCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<CardDocument>.Filter.Eq(d => d.Id, preparedEntity.Id);
            var documentToUpdate = CardDocument.InstanceOf(preparedEntity);
            await collection.ReplaceOneAsync(filter, documentToUpdate);
            return new Unit();
        }
    }
}
