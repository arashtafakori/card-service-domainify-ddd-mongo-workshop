using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    internal class FindCard :
        QueryItemRequestById<Card, string, Card?>
    {
        public FindCard(string id, bool evenDeletedData = false) : base(id)
        {
            EvenDeletedData = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
