using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
{
    public class EmptyCardsTrashHandler :
        IRequestHandler<EmptyCardsTrash>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EmptyCardsTrashHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            EmptyCardsTrash request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);
            await request.ResolveAsync(_mediator);

            var filter = Builders<CardDocument>.Filter.And(
                Builders<CardDocument>.Filter.Eq(d => d.IsDeleted, true),
                Builders<CardDocument>.Filter.Eq(d => d.BookletId, request.BookletId));

            await collection.DeleteManyAsync(filter);

            return new Unit();
        }
    }
}
