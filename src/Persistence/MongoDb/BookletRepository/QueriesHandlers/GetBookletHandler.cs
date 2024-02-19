using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    public class GetBookletHandler :
        IRequestHandler<GetBooklet, BookletViewModel?>
    {
        private readonly IMongoDatabase _database;
        public GetBookletHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<BookletViewModel?> Handle(
            GetBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>
                .Filter.Eq(r => r.Id, request.Id);

            return (await collection.FindSync(filter)
                .FirstOrDefaultAsync()).ToViewModel();
        }
    }
}
