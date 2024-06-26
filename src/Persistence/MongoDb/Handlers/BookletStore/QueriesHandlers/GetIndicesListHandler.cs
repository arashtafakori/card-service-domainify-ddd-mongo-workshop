﻿using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class GetIndicesListHandler :
        IRequestHandler<GetIndicesList,
            List<IndexViewModel>>
    {
        private readonly IMongoDatabase _database;
        public GetIndicesListHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<List<IndexViewModel>> Handle(
            GetIndicesList request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var filterBooklet = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);

            filterBooklet = filterBooklet & Builders<BookletDocument>
                .Filter.Eq(b => b.IsDeleted, false);

            var retrivalDeletationStatus = request.IsDeleted ?? false;

            var filter = Builders<BookletDocument>.Filter.And(filterBooklet);

            var booklet = await collection.FindSync(filter)
                .FirstOrDefaultAsync();

            var retrievedItems = new List<IndexViewModel>();
            if (booklet != null)
                retrievedItems = booklet.Indices
                    .Where(i => i.IsDeleted == retrivalDeletationStatus)
                    .Select(i => i.ToViewModel()).ToList();

            return retrievedItems;
        }
    }
}
