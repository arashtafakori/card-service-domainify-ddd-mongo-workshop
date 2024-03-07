using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    public class EmptyBookletsTrashHandler :
        IRequestHandler<EmptyBookletsTrash>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EmptyBookletsTrashHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            EmptyBookletsTrash request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>.Filter
               .Eq(d => d.IsDeleted, true);
            await collection.DeleteManyAsync(filter);

            return new Unit();
        }
    }
}
