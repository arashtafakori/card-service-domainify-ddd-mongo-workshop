using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    internal class MaxOrderValueOfCardInIndexHandler :
        IRequestHandler<MaxOrderValueOfCardInIndex, double>
    {
        private readonly IMongoDatabase _database;
        public MaxOrderValueOfCardInIndexHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<double> Handle(
            MaxOrderValueOfCardInIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);

            var filter = Builders<CardDocument>.Filter.And(
                Builders<CardDocument>.Filter.Eq(b => b.BookletId, request.BookletId),
                Builders<CardDocument>.Filter.Eq(b => b.IndexId, request.IndexId));

            var result = await collection.Find(filter)
                .SortByDescending(d => d.Order).Limit(1).FirstOrDefaultAsync();
            if(result != null)
                return result.Order;
            else return 0;
        }
    }
}
