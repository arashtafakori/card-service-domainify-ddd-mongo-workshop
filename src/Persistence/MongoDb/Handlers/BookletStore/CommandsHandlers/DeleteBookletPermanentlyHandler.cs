using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class DeleteBookletPermanentlyHandler :
        IRequestHandler<DeleteBookletPermanently>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public DeleteBookletPermanentlyHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            DeleteBookletPermanently request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter
               .Eq(d => d.Id, preparedEntity.Id);
            await collection.DeleteOneAsync(filter);

            return new Unit();
        }
    }
}
