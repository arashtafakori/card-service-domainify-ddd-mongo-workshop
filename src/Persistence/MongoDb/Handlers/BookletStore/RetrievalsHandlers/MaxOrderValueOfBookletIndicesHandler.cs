using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Index = Domain.BookletAggregation.Index;

namespace Persistence.BookletStore
{
    internal class MaxOrderValueOfIndexInBookletHandler :
        IRequestHandler<MaxOrderValueOfIndexInBooklet, double>
    {
        private readonly IMongoDatabase _database;
        public MaxOrderValueOfIndexInBookletHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<double> Handle(
            MaxOrderValueOfIndexInBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var filterBooklet = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);
            var filterIndex = Builders<Index>.Filter.Empty;

            var filter = Builders<BookletDocument>.Filter.And(filterBooklet,
                Builders<BookletDocument>.Filter.ElemMatch(b => b.Indices,
                Builders<Index>.Filter.And(filterIndex)));

            double maxOrder = 0;
            var result = await collection.FindSync(filter)
                .FirstOrDefaultAsync();

            if(result != null && result.Indices != null)
                maxOrder = result.Indices.Max(i => i.Order);

            return maxOrder;
        }
    }
}
