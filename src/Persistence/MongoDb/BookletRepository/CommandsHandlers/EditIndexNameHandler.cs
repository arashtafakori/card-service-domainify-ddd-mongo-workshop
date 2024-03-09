using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    public class EditIndexNameHandler :
        IRequestHandler<EditIndexName>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public EditIndexNameHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<Unit> Handle(
            EditIndexName request,
            CancellationToken cancellationToken)
        {
            //var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            //var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            //var filter = Builders<BookletDocument>.Filter
            //    .ElemMatch(t => t.Indices, i => i.Id == preparedItem.Id);

            //var indexOfArray = (await collection.FindAsync(filter)
            //    .Result.FirstOrDefaultAsync())
            //    .Indices.FindIndex(i => i.Id == preparedItem.Id);

            //var update = Builders<BookletDocument>.Update
            //    .Set("indices.$[elementIndex].Name", preparedItem.Name);

            //var arrayFilters = new List<ArrayFilterDefinition>
            //{
            //    new BsonDocumentArrayFilterDefinition<BookletDocument>(
            //        new BsonDocument("elementIndex._id", preparedItem.Id)

            //        )
            //};

            //var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });

            //if (result.ModifiedCount > 0)
            //{
            //    Console.WriteLine("Document updated successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("No document was updated.");
            //}

            //return new Unit();

            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter
                .ElemMatch(t => t.Indices, i => i.Id == preparedItem.Id);

            var indexOfArray = (await collection.FindAsync(filter)
                .Result.FirstOrDefaultAsync())
                .Indices.FindIndex(i => i.Id == preparedItem.Id);

            var update = Builders<BookletDocument>.Update
                .Set(t => t.Indices[indexOfArray].Name, preparedItem.Name);

            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
