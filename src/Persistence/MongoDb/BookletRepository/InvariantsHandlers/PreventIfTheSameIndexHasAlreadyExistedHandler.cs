using Domainify.Domain;
using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Index = Module.Domain.BookletAggregation.Index;

namespace Domainify.MongoDb.Datastore
{
    internal class PreventIfTheSameIndexHasAlreadyExistedHandler :
        IRequestHandler<PreventIfTheSameIndexHasAlreadyExisted, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public PreventIfTheSameIndexHasAlreadyExistedHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<bool> Handle(
            PreventIfTheSameIndexHasAlreadyExisted request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            await request.ResolveAsync(_mediator);

            var bookletId = request.BookletId;

            if (request.BookletId == null && request.Index.Id != null)
            {
                bookletId = (await collection.FindAsync(
                    Builders<BookletDocument>.Filter
                    .ElemMatch(i => i.Indices, i => i.Id == request.Index.Id))
                    .Result.FirstOrDefaultAsync()).Id;
            }

            var filter = Builders<Index>.Filter.And(
                    Builders<Index>.Filter.Eq(i => i.Name, request.Index.Name),
                    Builders<Index>.Filter.Eq(i => i.IsDeleted, false));

            if (!string.IsNullOrEmpty(request.Index.Id))
            {
                filter = filter & Builders<Index>.Filter.And(
                 Builders<Index>.Filter.Ne(d => d.Id, request.Index.Id));
            }

            var uniquenessFilter = Builders<BookletDocument>.Filter.And(
                Builders<BookletDocument>.Filter.Eq(b => b.Id, bookletId),
                Builders<BookletDocument>.Filter.ElemMatch(b => b.Indices, filter));

            if (request.Index.Uniqueness() != null && request.Index.Uniqueness()!.Condition != null)
            {
                await new LogicalState().AddAnPreventer(new PreventIfTheEntityHasAlreadyExistedPreventer
                                    <Index, BookletDocument>(collection, uniquenessFilter)
                                    .WithDescription(request.Index.Uniqueness()!.Description!))
                    .AssesstAsync();
            }

            return false;
        }
    }
}
