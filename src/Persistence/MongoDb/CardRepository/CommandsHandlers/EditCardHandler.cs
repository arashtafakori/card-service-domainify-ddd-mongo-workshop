using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
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
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<CardDocument>.Filter.Eq(d => d.Id, preparedItem.Id);
            var documentToUpdate = CardDocument.InstanceOf(preparedItem);
            await collection.ReplaceOneAsync(filter, documentToUpdate);
            return new Unit();
        }
    }
}
