using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.MongoDb;
using System.Linq;
using Index = Module.Domain.BookletAggregation.Index;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filter = Builders<BookletDocument>.Filter
                .ElemMatch(i => i.Indices, i => i.Id == request.Id);

            var indices = (await collection.FindAsync(filter)
                .Result.FirstOrDefaultAsync()).Indices.AsEnumerable();

            if (request.EvenDeletedData == false)
                indices = indices.Where(i => i.IsDeleted == false);

            return indices.FirstOrDefault(i => i.Id == request.Id);
        }
    }
}
