using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Domain.CardAggregation;

namespace Persistence
{
    internal class CardDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }
        public required double Version { get; set; }

        public required short Type { get; set; }
        public required string BookletId { get; set; }
        public string? IndexId { get; set; }
        public required double Order { get; set; }
        public required string Expression { get; set; }
        public required string ExpressionLanguage { get; set; }
        public string? Translation { get; set; }
        public string? TranslationLanguage { get; set; }
        public string? Description { get; set; }
        public static CardDocument InstanceOf(Card card)
        {
            var dataModel = new CardDocument()
            {
                Id = card.Id,
                IsDeleted = card.IsDeleted,
                ModifiedDate = card.ModifiedDate,
                Version = card.Version,

                Type = card.Type,
                BookletId = card.BookletId,
                IndexId = card.IndexId,
                Order = card.Order,
                Expression = card.Expression,
                ExpressionLanguage = card.ExpressionLanguage,
                Translation = card.Translation,
                TranslationLanguage = card.TranslationLanguage,
                Description = card.Description
            };
  
            return dataModel;
        }

        public Card ToEntity()
        {
            var card = Card.NewInstance(
                bookletId: BookletId,
                indexId: IndexId);
            card.SetId(Id!);
            card.ModifiedDate = ModifiedDate;
            card.IsDeleted = IsDeleted;
            card.Version = Version;

            card.SetType(Type)
                .SetOrder(Order)
                .SetExpression(Expression)
                .SetExpressionLanguage(ExpressionLanguage)
                .SetTranslation(Translation!)
                .SetTranslationLanguage(TranslationLanguage!)
                .SetDescription(Description!);
            return card;
        }
    }
}
