using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    public class EmptyCardsTrash :
        CommandRequest<Card>
    {
        [BindTo(typeof(Card), nameof(Card.BookletId))]
        public string BookletId { get; private set; }
        public EmptyCardsTrash(string bookletId)
        {
            BookletId = bookletId;
            ValidationState.Validate();
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
