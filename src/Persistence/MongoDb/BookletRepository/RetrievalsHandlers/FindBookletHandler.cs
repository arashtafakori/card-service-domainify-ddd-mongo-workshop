using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    internal class FindBookletHandler :
        IRequestHandler<FindBooklet, Booklet?>
    {
        private readonly IMongoDatabase _database;
        public FindBookletHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Booklet?> Handle(
            FindBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>
                .Filter.Eq(r => r.Id, request.Id);

            if (request.EvenDeletedData == false)
                filter = filter & Builders<BookletDocument>
                   .Filter.Eq(r => r.IsDeleted, request.EvenDeletedData);

            var findFluent = collection.Find(filter);

            if(request.WithIndices == false)
                findFluent = findFluent.Project<BookletDocument>(Builders<BookletDocument>.Projection.Exclude(r => r.Indices));

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
