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
        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }

        public static BookletDocument InstantiateFrom(Booklet booklet)
        {
            var dataModel = new BookletDocument();
            dataModel.Title = booklet.Title;

            dataModel.IsArchived = Convert.ToBoolean(booklet.Deleted);
            dataModel.CreatedDate = booklet.CreatedDate;
            dataModel.Title = booklet.Title;

            return dataModel;
        }

        public BookletViewModel ToViewModel()
        {
            var viewModel = new BookletViewModel()
            {
                Id = Id!, 
                Title = Title,
                IsArchived = IsArchived,
                ModifiedDate = ModifiedDate
            };

            return viewModel;
        }
    }
}
