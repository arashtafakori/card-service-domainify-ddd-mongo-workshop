﻿using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class EmptyIndicesTrashHandler :
        IRequestHandler<EmptyIndicesTrash>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EmptyIndicesTrashHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            EmptyIndicesTrash request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            await request.ResolveAsync(_mediator);

            var filter = Builders<BookletDocument>
                   .Filter.Eq(b => b.Id, request.BookletId);

            var update = Builders<BookletDocument>.Update
                .PullFilter(b => b.Indices, i => i.IsDeleted == true);

            await collection.UpdateManyAsync(filter, update);
            return new Unit();
        }
    }
}
