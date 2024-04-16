using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class DeleteIndexPermanentlyHandler :
        IRequestHandler<DeleteIndexPermanently>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public DeleteIndexPermanentlyHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            DeleteIndexPermanently request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter
                .ElemMatch(t => t.Indices, i => i.Id == preparedEntity.Id);

            var update = Builders<BookletDocument>.Update
                .PullFilter(b => b.Indices, i => i.Id == preparedEntity.Id);

            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
