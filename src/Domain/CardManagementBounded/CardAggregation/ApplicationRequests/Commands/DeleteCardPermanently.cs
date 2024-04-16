using Domainify.Domain;
using MediatR;

namespace Domain.CardAggregation
{
    public class DeleteCardPermanently :
        RequestToDeletePermanentlyById<Card, string>
    {
        public DeleteCardPermanently(string id)
            : base(id)
        { 
            ValidationState.Validate();
        }

        public override async Task<Card> ResolveAndGetEntityAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var card = (await mediator.Send(
                new FindCard(Id, includeDeleted: true)))!;
            await base.ResolveAsync(mediator, card);
            return card;
        }
    }
}
