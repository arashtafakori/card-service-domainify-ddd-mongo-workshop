using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Domain.CardAggregation;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardAggregation
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
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);

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
