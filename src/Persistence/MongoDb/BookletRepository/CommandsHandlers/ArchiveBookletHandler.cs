using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;

namespace Module.Persistence.BookletRepository
{
    public class ArchiveBookletHandler :
        IRequestHandler<ArchiveBooklet>
    {
        private readonly IMediator _mediator;
        private readonly IMongoDatabase _database;
        public ArchiveBookletHandler(
            IMediator mediator, IMongoDatabase database)
        {
            _mediator = mediator;
            _database = database;
        }

        public async Task<Unit> Handle(
            ArchiveBooklet request,
            CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);
            var entity = await request.ResolveAndGetEntityAsync(_mediator);

            var filter = Builders<BookletDocument>.Filter.Eq(d => d.Id, entity.Id);
            
            var update = Builders<BookletDocument>.Update.Set(d => d.IsArchived, entity.IsArchived);
            await collection.UpdateOneAsync(filter, update);
            return new Unit();
        }
    }
}
