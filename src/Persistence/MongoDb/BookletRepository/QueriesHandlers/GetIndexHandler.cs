using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Index = Module.Domain.BookletAggregation.Index;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

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
