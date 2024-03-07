using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Domainify.Domain;
using Domainify.MongoDb.Datastore;

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
            var entity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, entity.Id);
            var bookletDoc = BookletDocument.InstanceOf(entity);
            await collection.ReplaceOneAsync(filter, bookletDoc);
            return new Unit();
        }
    }
}
