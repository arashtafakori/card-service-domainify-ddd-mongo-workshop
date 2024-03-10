using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Module.Domain.BookletAggregation;
using Index = Module.Domain.BookletAggregation.Index;

namespace Persistence.MongoDb
{
    internal class BookletDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required bool IsDeleted { get; set; } = false;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public required DateTime ModifiedDate { get; set; }

        public required short Type { get; set; }
        public required string Title { get; set; }

        public List<Index> Indices { get; set; } = new List<Index>();

        public static BookletDocument InstanceOf(Booklet booklet)
        {
            var dataModel = new BookletDocument()
            {
                Id = booklet.Id,
                IsDeleted = booklet.IsDeleted,
                ModifiedDate = booklet.ModifiedDate,

                Type = booklet.Type,
                Title = booklet.Title,
            };
  
            return dataModel;
        }

        public Booklet ToEntity()
        {
            var booklet = Booklet.NewInstance(Type);
            booklet.SetId(Id!);
            booklet.ModifiedDate = ModifiedDate;
            booklet.IsDeleted = IsDeleted;

            booklet.SetTitle(Title!);
            booklet.Indices = Indices;

            return booklet;
        }
    }
}
