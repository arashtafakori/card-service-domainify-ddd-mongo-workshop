using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    public class AddCard
        : RequestToCreate<Card, string>
    {
        [BindTo(typeof(Card), nameof(Card.BookletId))]
        public string BookletId { get; private set; }

        [BindTo(typeof(Card), nameof(Card.IndexId))]
        public string? IndexId { get; private set; }

        [BindTo(typeof(Card), nameof(Card.Expression))]
        public string Expression { get; private set; }

        [BindTo(typeof(Card), nameof(Card.ExpressionLanguage))]
        public string ExpressionLanguage { get; private set; }

        [BindTo(typeof(Card), nameof(Card.Translation))]
        public string? Translation { get; private set; }

        [BindTo(typeof(Card), nameof(Card.TranslationLanguage))]
        public string? TranslationLanguage { get; private set; }

        public AddCard(
            string bookletId,
            string expression,
            string expressionLanguage,
            string? translation = null,
            string? translationLanguage = null,
            string? indexId = null)
        {
            BookletId = bookletId;
            IndexId = indexId;
            Expression = expression.Trim();
            ExpressionLanguage = expressionLanguage.Trim();
            Translation = translation!.Trim();
            TranslationLanguage = translationLanguage!.Trim();

            ValidationState.Validate();
        }

        public override async Task<Card> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var card = Card.NewInstance(type: 1, bookletId: BookletId, indexId: IndexId)
                .SetExpression(Expression)
                .SetExpressionLanguage(ExpressionLanguage)
                .SetTranslation(Translation!)
                .SetTranslationLanguage(TranslationLanguage!);

            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, card);
            return card;
        }
    }
}
