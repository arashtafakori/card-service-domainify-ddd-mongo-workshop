using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Domainify.MongoDb.Datastore
{
    internal class PreventIfTheSameBookletHasAlreadyExistedHandler :
        IRequestHandler<PreventIfTheSameBookletHasAlreadyExisted, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public PreventIfTheSameBookletHasAlreadyExistedHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<bool> Handle(
            PreventIfTheSameBookletHasAlreadyExisted request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            await request.ResolveAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.And(
                Builders<BookletDocument>.Filter.Eq(d => d.IsDeleted, false),
                Builders<BookletDocument>.Filter.Eq(d => d.Title, request.Booklet.Title));

            if (!string.IsNullOrEmpty(request.Booklet.Id))
            {
                filter = filter & Builders<BookletDocument>.Filter.And(
                 Builders<BookletDocument>.Filter.Ne(d => d.Id, request.Booklet.Id));
            }

            if (request.Booklet.Uniqueness() != null && request.Booklet.Uniqueness()!.Condition != null)
                return await collection.Find(filter).AnyAsync();

            return false;
        }
    }
}
