using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    public class EmptyCardsTrash :
        CommandRequest<Card>
    {
        [BindTo(typeof(Card), nameof(Card.BookletId))]
        public required string BookletId { get; set; }

        [BindTo(typeof(Card), nameof(Card.IndexId))]
        public string? IndexId { get; set; }

        public EmptyCardsTrash()
        {
            ValidationState.Validate();
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
