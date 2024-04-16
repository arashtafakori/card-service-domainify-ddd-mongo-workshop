using MediatR;
using Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Persistence.CardStore
{
    public class AddCardHandler :
        IRequestHandler<AddCard, string>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public AddCardHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<string> Handle(
            AddCard request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<CardDocument>(CollectionNames.Cards);
            var preparedEntity = await request.ResolveAndGetEntityAsync(_mediator);

            var newDocument = CardDocument.InstanceOf(preparedEntity);
            await collection.InsertOneAsync(newDocument);
            preparedEntity.SetId(newDocument.Id!);
            return preparedEntity.Id;
        }
    }
} 
