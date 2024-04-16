using Domainify.Domain;
using MediatR;

namespace Domain.CardAggregation
{
    public class EditCard :
        RequestToUpdateById<Card, string>
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

        [BindTo(typeof(Card), nameof(Card.Description))]
        public string? Description { get; private set; }
        public EditCard(
            string id,
            string bookletId,
            long order,
            string expression,
            string expressionLanguage,
            string? translation = null,
            string? translationLanguage = null,
            string? description = null,
            string? indexId = null) 
            : base(id)
        {
            BookletId = bookletId;
            IndexId = indexId;
            Expression = expression.Trim();
            ExpressionLanguage = expressionLanguage.Trim();
            Translation = translation!.Trim();
            TranslationLanguage = translationLanguage!.Trim();
            Description = description!.Trim();

            ValidationState.Validate();
        }

        public override async Task<Card> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var card = (await mediator.Send(new FindCard(Id)))!;
            card.SetBookletId(BookletId)
                .SetIndexId(IndexId)
                .SetExpression(Expression)
                .SetExpressionLanguage(ExpressionLanguage)
                .SetTranslation(Translation!)
                .SetTranslationLanguage(TranslationLanguage!)
                .SetDescription(Description!);

            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, card);
            return card;
        }
    }
}
