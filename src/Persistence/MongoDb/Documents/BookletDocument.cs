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
        public string? Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }

        public List<Index> Indices { get; set; } = new List<Index>();

        public static BookletDocument InstanceOf(Booklet booklet)
        {
            var dataModel = new BookletDocument()
            {
                Type = booklet.Type,
                ModifiedDate = booklet.ModifiedDate,
                IsDeleted = booklet.IsDeleted,
                Id = booklet.Id,

                Title = booklet.Title,
            };
  
            return dataModel;
        }

        public Booklet ToEntity()
        {
            var booklet = Booklet.NewInstance(Type);
            booklet.ModifiedDate = ModifiedDate;
            booklet.IsDeleted = IsDeleted;

            booklet.SetId(Id!);
            booklet.SetTitle(Title!);
            booklet.Indices = Indices;

            return booklet;
        }
    }
}
