using MediatR;
using Module.Domain.BookletAggregation;
using MongoDB.Driver;
using Persistence.MongoDb;
using Domainify.Domain;
using MongoDB.Bson;
using Index = Module.Domain.BookletAggregation.Index;
using System.Collections.Generic;

namespace Module.Persistence.BookletRepository
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
            var collection = _database.GetCollection<BookletDocument>(ConnectionNames.Booklet);

            var filterBooklet = Builders<BookletDocument>.Filter.Eq(b => b.Id, request.BookletId);

            var retrivalDeletationStatus = request.IsDeleted;
            if (request.IsDeleted == false && request.EvenDeletedData)
                retrivalDeletationStatus = true;

            filterBooklet = filterBooklet & Builders<BookletDocument>.Filter.Eq(b => b.IsDeleted, retrivalDeletationStatus);
            var filterIndex =Builders<Index>.Filter.Eq(i => i.IsDeleted, retrivalDeletationStatus);

            var filter = Builders<BookletDocument>.Filter.And(filterBooklet,
                Builders<BookletDocument>.Filter.ElemMatch(b => b.Indices,
                Builders<Index>.Filter.And(filterIndex, 
                Builders<Index>.Filter.Regex(
                    d => d.Name, new BsonRegularExpression(request.SearchValue, "i")))));

            var booklet = (await collection.FindSync(filter)
                .FirstOrDefaultAsync());

            var indices = new List<IndexViewModel>();
            if (booklet != null)
                indices = booklet.Indices.Select(i => i.ToViewModel()).ToList();

            return indices;
        }
    }
}
