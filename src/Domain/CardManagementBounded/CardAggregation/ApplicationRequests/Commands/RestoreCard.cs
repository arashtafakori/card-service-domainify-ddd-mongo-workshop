using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    public class RestoreCard :
        RequestToRestoreById<Card, string>
    {
        public RestoreCard(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Card> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var card = (await mediator.Send(
                new FindCard(Id, evenDeletedData: true)))!;

            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, card);
            return card;
        }
    }
}
