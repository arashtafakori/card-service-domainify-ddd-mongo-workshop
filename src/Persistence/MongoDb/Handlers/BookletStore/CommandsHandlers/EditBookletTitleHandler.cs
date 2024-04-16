using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class EditBookletTitleHandler :
        IRequestHandler<EditBookletTitle>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EditBookletTitleHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<Unit> Handle(
            EditBookletTitle request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, preparedEntity.Id);
            var documentToUpdate = BookletDocument.InstanceOf(preparedEntity);
            await collection.ReplaceOneAsync(filter, documentToUpdate);
            return new Unit();
        }
    }
}
