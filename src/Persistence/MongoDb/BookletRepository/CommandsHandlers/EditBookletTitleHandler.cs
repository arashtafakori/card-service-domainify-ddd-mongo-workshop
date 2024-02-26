using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Domain;
using XSwift.MongoDb.Datastore;

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

            var logicalState = new LogicalState();
            var uniquenessFilter = Builders<BookletDocument>.Filter.And(
                Builders<BookletDocument>.Filter.Eq(d => d.IsArchived, false),
                Builders<BookletDocument>.Filter.Eq(d => d.Title, request.Title),
                Builders<BookletDocument>.Filter.Ne(d => d.Id, request.Id));
            if (entity.Uniqueness() != null && entity.Uniqueness()!.Condition != null)
            {
                await new LogicalState().AddAnPreventer(new PreventIfTheEntityHasAlreadyExistedPreventer
                                    <Booklet, BookletDocument>(collection, uniquenessFilter)
                                    .WithDescription(entity.Uniqueness()!.Description!))
                    .AssesstAsync();
            }

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, entity.Id);
            var bookletDoc = BookletDocument.InstanceOf(entity);
            await collection.ReplaceOneAsync(filter, bookletDoc);
            return new Unit();
        }
    }
}
