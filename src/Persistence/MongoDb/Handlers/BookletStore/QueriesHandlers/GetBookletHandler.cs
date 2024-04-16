using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class GetBookletHandler :
        IRequestHandler<GetBooklet, Booklet?>
    {
        private readonly IMongoDatabase _database;
        public GetBookletHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Booklet?> Handle(
            GetBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var filter = Builders<BookletDocument>
                .Filter.Eq(b => b.Id, request.Id);

            if (request.IncludeDeleted == false)
                filter = filter & Builders<BookletDocument>
                   .Filter.Eq(b => b.IsDeleted, request.IncludeDeleted);

            var findFluent = collection.Find(filter);

            if(request.WithIndices == false)
                findFluent = findFluent.Project<BookletDocument>(Builders<BookletDocument>.Projection.Exclude(r => r.Indices));

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
