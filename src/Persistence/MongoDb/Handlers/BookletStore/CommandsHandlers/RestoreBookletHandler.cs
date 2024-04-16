using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class RestoreBookletHandler :
        IRequestHandler<RestoreBooklet>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public RestoreBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            RestoreBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, preparedEntity.Id);
            var update = Builders<BookletDocument>.Update.Set(d => d.IsDeleted, preparedEntity.IsDeleted);
            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
