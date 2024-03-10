using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Module.Domain.CardAggregation;
 
namespace Persistence.MongoDb
{
    internal class CardDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }

        public required string BookletId { get; set; }
        public string? IndexId { get; set; }
        public required short Type { get; set; }
        public required long Order { get; set; }
        public required string Expression { get; set; }
        public required string ExpressionLanguage { get; set; }
        public string? Translation { get; set; }
        public string? TranslationLanguage { get; set; }
        public static CardDocument InstanceOf(Card card)
        {
            var dataModel = new CardDocument()
            {
                Id = card.Id,
                IsDeleted = card.IsDeleted,
                ModifiedDate = card.ModifiedDate,

                BookletId = card.BookletId,
                IndexId = card.IndexId,
                Order = card.Order,
                Type = card.Type,
                Expression = card.Expression,
                ExpressionLanguage = card.ExpressionLanguage,
                Translation = card.Translation,
                TranslationLanguage = card.TranslationLanguage,
            };
  
            return dataModel;
        }

        public Card ToEntity()
        {
            var card = Card.NewInstance(
                type: Type,
                bookletId: BookletId,
                indexId: IndexId);
            card.SetId(Id!);
            card.ModifiedDate = ModifiedDate;
            card.IsDeleted = IsDeleted;

            card.SetOrder(Order)
                .SetExpression(Expression)
                .SetExpressionLanguage(ExpressionLanguage)
                .SetTranslation(Translation!)
                .SetTranslationLanguage(TranslationLanguage!);

            return card;
        }
    }
}
