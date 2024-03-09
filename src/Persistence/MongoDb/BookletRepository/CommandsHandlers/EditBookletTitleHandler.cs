using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, preparedItem.Id);
            var bookletDoc = BookletDocument.InstanceOf(preparedItem);
            await collection.ReplaceOneAsync(filter, bookletDoc);
            return new Unit();
        }
    }
}
