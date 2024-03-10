using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
{
    public class DeleteCardPermanentlyHandler :
        IRequestHandler<DeleteCardPermanently>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public DeleteCardPermanentlyHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            DeleteCardPermanently request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<CardDocument>.Filter
               .Eq(d => d.Id, preparedItem.Id);
            await collection.DeleteOneAsync(filter);

            return new Unit();
        }
    }
}
