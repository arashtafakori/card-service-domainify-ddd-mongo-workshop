using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class RestoreIndexHandler :
        IRequestHandler<RestoreIndex>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public RestoreIndexHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            RestoreIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter
                .ElemMatch(t => t.Indices, i => i.Id == preparedEntity.Id);

            var indexOfArray = (await collection.FindAsync(filter)
                .Result.FirstOrDefaultAsync())
                .Indices.FindIndex(i => i.Id == preparedEntity.Id);

            var update = Builders<BookletDocument>.Update
                .Set(t => t.Indices[indexOfArray].IsDeleted, preparedEntity.IsDeleted);

            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
