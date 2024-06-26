﻿using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    internal class FindBookletHandler :
        IRequestHandler<FindBooklet, Booklet?>
    {
        private readonly IMongoDatabase _database;
        public FindBookletHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Booklet?> Handle(
            FindBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);

            var filter = Builders<BookletDocument>
                .Filter.Eq(r => r.Id, request.Id);

            if (request.IncludeDeleted == false)
                filter = filter & Builders<BookletDocument>
                   .Filter.Eq(r => r.IsDeleted, request.IncludeDeleted);

            var findFluent = collection.Find(filter);

            if(request.WithIndices == false)
                findFluent = findFluent.Project<BookletDocument>(Builders<BookletDocument>.Projection.Exclude(r => r.Indices));

            return (await findFluent
                .FirstOrDefaultAsync()).ToEntity();
        }
    }
}
