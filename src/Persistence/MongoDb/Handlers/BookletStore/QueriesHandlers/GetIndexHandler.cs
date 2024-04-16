using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Index = Domain.BookletAggregation.Index;

namespace Persistence.BookletStore
{
    public class GetIndexHandler :
        IRequestHandler<GetIndex, Index?>
    {
        private readonly IMongoDatabase _database;
        public GetIndexHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Index?> Handle(
            GetIndex request,
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
