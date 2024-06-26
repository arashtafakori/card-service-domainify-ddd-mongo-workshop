﻿using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using MongoDB.Bson;

namespace Persistence.BookletStore
{
    public class AddIndexHandler :
        IRequestHandler<AddIndex, (string bookletId, string id)?>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public AddIndexHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<(string bookletId, string id)?> Handle(
            AddIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);
            preparedEntity.SetId(ObjectId.GenerateNewId().ToString());

            var filter = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);
            var update = Builders<BookletDocument>.Update.Push(b => b.Indices, preparedEntity);
        
            var options = new FindOneAndUpdateOptions<BookletDocument>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatedItem = await collection.FindOneAndUpdateAsync(filter, update, options);

            if (updatedItem != null && updatedItem.Indices.Any())
                return (request.BookletId, updatedItem.Indices.Last().Id);

            return null;
        }
    }
} 
