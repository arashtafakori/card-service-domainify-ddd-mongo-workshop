using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Datastore;
using XSwift.Domain;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace Module.Persistence.BookletRepository
{
    public class GetBookletListHandler :
        IRequestHandler<GetBookletList,
            PaginatedViewModel<BookletViewModel>>
    {
        private readonly IMongoDatabase _database;
        public GetBookletListHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<PaginatedViewModel<BookletViewModel>> Handle(
            GetBookletList request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>.Filter.Empty;
            var items = (await collection.Find(filter).ToListAsync())
                .Select(booklet => booklet.ToViewModel()).ToList();

            return new PaginatedViewModel<BookletViewModel>(items, items.Count());
        }
    }
}
