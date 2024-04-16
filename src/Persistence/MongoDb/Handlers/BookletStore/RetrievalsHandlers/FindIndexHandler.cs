using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.MongoDb;
using Index = Domain.BookletAggregation.Index;

namespace Persistence.BookletStore
{
    internal class FindIndexHandler :
        IRequestHandler<FindIndex, Index?>
    {
        private readonly IMongoDatabase _database;
        public FindIndexHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Index?> Handle(
            FindIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var filter = Builders<BookletDocument>.Filter
                .ElemMatch(i => i.Indices, i => i.Id == request.Id);

            var indices = (await collection.FindAsync(filter)
                .Result.FirstOrDefaultAsync()).Indices.AsEnumerable();

            if (request.IncludeDeleted == false)
                indices = indices.Where(i => i.IsDeleted == false);

            return indices.FirstOrDefault(i => i.Id == request.Id);
        }
    }
}
