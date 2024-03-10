using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardAggregation
{
    internal class MaxOrderValueOfCardInIndexHandler :
        IRequestHandler<MaxOrderValueOfCardInIndex, long>
    {
        private readonly IMongoDatabase _database;
        public MaxOrderValueOfCardInIndexHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<long> Handle(
            MaxOrderValueOfCardInIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);

            var filter = Builders<CardDocument>.Filter.Eq(b => b.Id, request.IndexId);

            return await collection.AsQueryable()
                        .Select(c => c.Order)
                        .MaxAsync();
        }
    }
}
