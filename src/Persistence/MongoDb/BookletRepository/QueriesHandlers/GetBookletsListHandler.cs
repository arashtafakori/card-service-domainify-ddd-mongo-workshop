using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Domainify.Domain;
using MongoDB.Bson;

namespace Module.Persistence.BookletRepository
{
    public class GetBookletsListHandler :
        IRequestHandler<GetBookletsList,
            PaginatedList<BookletViewModel>>
    {
        private readonly IMongoDatabase _database;
        public GetBookletsListHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PaginatedList<BookletViewModel>> Handle(
            GetBookletsList request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var retrivalDeletationStatus = request.IsDeleted ?? false;
            if (request.IsDeleted == false && request.IncludeDeleted)
                retrivalDeletationStatus = true;

            var filter = Builders<BookletDocument>.Filter.And(
            Builders<BookletDocument>.Filter.Eq(d => d.IsDeleted, retrivalDeletationStatus),
            Builders<BookletDocument>.Filter.Regex(d => d.Title, new BsonRegularExpression(request.SearchValue, "i")));

            var skip = request.PageNumber <= 1 ? 0 : ((request.PageNumber - 1) * request.PageSize) - 1;

            var findFluent = collection.Find(filter)
                .Project<BookletDocument>(Builders<BookletDocument>.Projection.Exclude(r => r.Indices))
                .Skip(skip)
                .Limit(request.PageSize)
                .SortByDescending(r => r.ModifiedDate);

             var retrievedItems = (await findFluent.ToListAsync())
                .Select(i => i.ToEntity().ToViewModel()).ToList();

            var totalCount = await findFluent.CountDocumentsAsync();

            return new PaginatedList<BookletViewModel>(
                retrievedItems,
                numberOfTotalItems: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize);
        }
    }
}
