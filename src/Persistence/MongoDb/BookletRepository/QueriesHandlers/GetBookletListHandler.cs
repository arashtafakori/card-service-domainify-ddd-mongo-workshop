using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Domain;

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

            //var filter = Builders<BookletDocument>.Filter.Empty;
            var filter = Builders<BookletDocument>.Filter.And(
                Builders<BookletDocument>.Filter.Eq(d => d.IsArchived, false));
            var items = (await collection.Find(filter)
                .SortByDescending(r => r.ModifiedDate).ToListAsync())
                .Select(booklet => booklet.ToViewModel()).ToList();

            return new PaginatedViewModel<BookletViewModel>(items, items.Count());
        }
    }
}
