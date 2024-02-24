using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Module.Domain.BookletAggregation;

namespace Persistence.MongoDb
{
    internal class BookletDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; } = null!;

        public bool IsArchived { get; set; } = false;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }

        public static BookletDocument InstanceOf(Booklet booklet)
        {
            var dataModel = new BookletDocument()
            {
                ModifiedDate = booklet.ModifiedDate,
                IsArchived = booklet.IsArchived,
                Id = booklet.Id,

                Title = booklet.Title,
            };
  
            return dataModel;
        }

        public BookletViewModel ToViewModel()
        {
            var viewModel = new BookletViewModel()
            {
                ModifiedDate = ModifiedDate,
                IsArchived = IsArchived,
                Id = Id!,
                Title = Title,
            };

            return viewModel;
        }

        public Booklet ToEntity()
        {
            var booklet = Booklet.Instantiate(ModifiedDate, IsArchived);
            booklet.SetId(Id!);
            booklet.SetTitle(Title!);

            return booklet;
        }
    }
}
