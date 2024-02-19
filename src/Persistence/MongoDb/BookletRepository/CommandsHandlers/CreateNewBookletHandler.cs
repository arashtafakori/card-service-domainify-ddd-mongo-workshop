using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using XSwift.Domain;
using XSwift.MongoDb.Datastore;

namespace Module.Persistence.BookletRepository
{
    public class CreateNewBookletHandler :
        IRequestHandler<CreateNewBooklet, string>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public CreateNewBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }
        public async Task<string> Handle(
            CreateNewBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            //---
            if (entity.Uniqueness() != null && entity.Uniqueness()!.Condition != null)
            {
                var filter = Builders<BookletDocument>
                    .Filter.Eq(r => r.Title, request.Title);

                await new LogicalState()
                    .AddAnPreventer(new PreventIfTheEntityWithTheseUniquenessConditionsHasAlreadyExisted
                    <Booklet, BookletDocument>(collection, filter)
                    .WithDescription(entity.Uniqueness()!.Description!))
                    .AssesstAsync();
            }
            //---
            var booklet = BookletDocument.InstantiateFrom(entity);
            await collection.InsertOneAsync(booklet);
            entity.SetId(booklet.Id!);
            return entity.Id;
        }
    }
} 
