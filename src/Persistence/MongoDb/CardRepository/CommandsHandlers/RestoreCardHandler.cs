﻿using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
{
    public class RestoreCardHandler :
        IRequestHandler<RestoreCard>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public RestoreCardHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            RestoreCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<CardDocument>.Filter.Eq(d => d.Id, preparedItem.Id);
            var update = Builders<CardDocument>.Update.Set(d => d.IsDeleted, preparedItem.IsDeleted);
            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
