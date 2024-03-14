using Domainify.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.CardAggregation
{
    public class Card : Entity<Card, string>, IAggregateRoot
    {
        public double Version { get; set; }

        [Required]
        public string BookletId { get; protected set; } = string.Empty;

        public string? IndexId { get; protected set; }
        public short Type { get; protected set; }

        [Required]
        public double Order { get; protected set; }

        [MinLengthShouldBe(1)]
        [Required(AllowEmptyStrings = false)]
        public string Expression { get; protected set; } = string.Empty;

        [MinLengthShouldBe(2)]
        [MaxLengthShouldBe(2)]
        [StringLength(2)]
        [Required(AllowEmptyStrings = false)]
        public string ExpressionLanguage { get; protected set; } = string.Empty;

        [MinLengthShouldBe(1)]
        public string? Translation { get; protected set; }

        [MinLengthShouldBe(2)]
        [MaxLengthShouldBe(2)]
        [StringLength(2)]
        public string? TranslationLanguage { get; protected set; }
        public Card()
        {
            Type = 1;
            Version = 1.0;
        }
        public static Card NewInstance(
            string bookletId,
            string? indexId = null)
        {
            return new Card()
                .SetBookletId(bookletId)
                .SetIndexId(indexId);
        }
        public Card SetType(short value)
        {
            Type = value;

            return this;
        }
        public Card SetOrder(double value)
        {
            Order = value;

            return this;
        }

        public Card SetBookletId(string value)
        {
            BookletId = value;

            return this;
        }
        public Card SetIndexId(string? value)
        {
            IndexId = value;

            return this;
        }
        public Card SetExpression(string value)
        {
            Expression = value;

            return this;
        }
        public Card SetExpressionLanguage(string value)
        {
            ExpressionLanguage = value;

            return this;
        }
        public Card SetTranslation(string value)
        {
            Translation = value;

            return this;
        }
        public Card SetTranslationLanguage(string value)
        {
            TranslationLanguage = value;

            return this;
        }


        public CardViewModel ToViewModel()
        {
            var viewModel = new CardViewModel()
            {
                Type = Type,
                ModifiedDate = ModifiedDate,
                IsDeleted = IsDeleted,
                Id = Id!,
                BookletId = BookletId,
                IndexId = IndexId!,
                Order = Order,
                Expression = Expression,
                ExpressionLanguage = ExpressionLanguage,
                Translation = Translation!,
                TranslationLanguage = TranslationLanguage!,
            };

            return viewModel;
        }
    }
}
