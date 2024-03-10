using MediatR;
using MongoDB.Driver;
using Persistence.MongoDb;
using Domainify.Domain;
using MongoDB.Bson;
using Module.Domain.CardAggregation;

namespace Module.Persistence.CardRepository
{
    public class GetCardsListHandler :
        IRequestHandler<GetCardsList,
            PaginatedViewModel<CardViewModel>>
    {
        private readonly IMongoDatabase _database;
        public GetCardsListHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PaginatedViewModel<CardViewModel>> Handle(
            GetCardsList request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);

            var retrivalDeletationStatus = request.IsDeleted;
            if (request.IsDeleted == false && request.EvenDeletedData)
                retrivalDeletationStatus = true;

            var filterSearch = Builders<CardDocument>.Filter.Or(
                Builders<CardDocument>.Filter.Regex(d => d.Translation, new BsonRegularExpression(request.SearchValue, "i")),
                Builders<CardDocument>.Filter.Regex(d => d.Expression, new BsonRegularExpression(request.SearchValue, "i")));

            var filter = Builders<CardDocument>.Filter.And(
                Builders<CardDocument>.Filter.Eq(d => d.IsDeleted, retrivalDeletationStatus),
                filterSearch);

            var skip = request.PageNumber <= 1 ? 0 : ((request.PageNumber - 1) * request.PageSize) - 1;

            var findFluent = collection.Find(filter)
                .Skip(skip)
                .Limit(request.PageSize)
                .SortByDescending(r => r.Order);

             var retrievedItems = (await findFluent.ToListAsync())
                .Select(i => i.ToEntity().ToViewModel()).ToList();

            var totalCount = await findFluent.CountDocumentsAsync();

            return new PaginatedViewModel<CardViewModel>(
                retrievedItems,
                numberOfTotalItems: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize);
        }
    }
}
