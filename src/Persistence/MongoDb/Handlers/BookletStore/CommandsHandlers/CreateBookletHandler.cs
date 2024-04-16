using MediatR;
using Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.BookletStore
{
    public class CreateBookletHandler :
        IRequestHandler<CreateBooklet, string>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public CreateBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<string> Handle(
            CreateBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(CollectionNames.Booklets);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            //await BookletAggregation.Setup(_mediator).CreateBooklet(preparedEntity);

            var newDocument = BookletDocument.InstanceOf(preparedEntity);
            await collection.InsertOneAsync(newDocument);
            //preparedEntity.SetId(newDocument.Id!);
            return newDocument.Id;
        }
    }
} 
