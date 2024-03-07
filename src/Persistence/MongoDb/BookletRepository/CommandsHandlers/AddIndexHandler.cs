using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Domainify.Domain;
using Domainify.MongoDb.Datastore;
using Index = Module.Domain.BookletAggregation.Index;
using MongoDB.Bson;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var index = await request.ResolveAndGetEntityAsync(_mediator);
            index.SetId(ObjectId.GenerateNewId().ToString());

            var filter = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);
            var update = Builders<BookletDocument>.Update.Push(b => b.Indices, index);
        
            var options = new FindOneAndUpdateOptions<BookletDocument>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatedBooklet = collection.FindOneAndUpdate(filter, update, options);

            if (updatedBooklet != null && updatedBooklet.Indices.Any())
                return (request.BookletId, updatedBooklet.Indices.Last().Id);

            return null;
        }
    }
} 
