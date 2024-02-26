using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Domain;
using XSwift.MongoDb.Datastore;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            var entity = await request.ResolveAndGetEntityAsync(_mediator);

            var logicalState = new LogicalState();
            var uniquenessFilter = Builders<BookletDocument>.Filter.And(
                Builders<BookletDocument>.Filter.Eq(d => d.IsArchived, false),
                Builders<BookletDocument>.Filter.Eq(d => d.Title, entity.Title),
                Builders<BookletDocument>.Filter.Ne(d => d.Id, request.Id));
            if (entity.Uniqueness() != null && entity.Uniqueness()!.Condition != null)
            {
                await new LogicalState().AddAnPreventer(new PreventIfTheEntityHasAlreadyExistedPreventer
                                    <Booklet, BookletDocument>(collection, uniquenessFilter)
                                    .WithDescription(entity.Uniqueness()!.Description!))
                    .AssesstAsync();
            }

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, entity.Id);
            var update = Builders<BookletDocument>.Update.Set(d => d.IsArchived, entity.IsArchived);
            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
