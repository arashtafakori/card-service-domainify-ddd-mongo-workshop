using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Index = Module.Domain.BookletAggregation.Index;

namespace Module.Persistence.BookletRepository
{
    public class GetIndexHandler :
        IRequestHandler<GetIndex, Index?>
    {
        private readonly IMongoDatabase _database;
        public GetIndexHandler(IMongoDatabase database) 
        {
            _database = database;
        }

        public async Task<Index?> Handle(
            GetIndex request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filterBooklet = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);
            var filterIndex = Builders<Index>.Filter.Eq(i => i.Id, request.Id);

            if (request.EvenDeletedData == false)
            {
                filterBooklet = filterBooklet & Builders<BookletDocument>.Filter.Eq(b => b.IsDeleted, request.EvenDeletedData);
                filterIndex = filterIndex & Builders<Index>.Filter.Eq(i => i.IsDeleted, request.EvenDeletedData);
            }

            var filter = Builders<BookletDocument>.Filter.And(filterBooklet,
                Builders<BookletDocument>.Filter.ElemMatch(b => b.Indices,
                Builders<Index>.Filter.And(filterIndex)));

            var index = (await collection.FindSync(filter)
                .FirstOrDefaultAsync()).Indices[0];

            //
            //var unwindStage = new BsonDocument("$unwind", "$Indices");
            //var matchStage = new BsonDocument("$match", new BsonDocument("Indices._id", request.Id));
            //var projectStage = new BsonDocument("$project", new BsonDocument
            //{
            //    { "_id", 0 },
            //    { "Index", "$Indices" }
            //});

            //var pipeline = PipelineDefinition<BookletDocument, Index>.Create(unwindStage, matchStage, projectStage);
            //var index = collection.Aggregate(pipeline).FirstOrDefault();

            return index;
        }
    }
}
