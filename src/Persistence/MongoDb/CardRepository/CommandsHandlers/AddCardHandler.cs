using MediatR;
using Module.Domain.CardAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.CardRepository
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
            var collection = _database.GetCollection<CardDocument>(ConnectionNames.Card);
            var preparedItem = await request.ResolveAndGetEntityAsync(_mediator);

            var newDocument = CardDocument.InstanceOf(preparedItem);
            await collection.InsertOneAsync(newDocument);
            preparedItem.SetId(newDocument.Id!);
            return preparedItem.Id;
        }
    }
} 
