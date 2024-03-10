using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    public class GetCard :
        QueryItemRequestById<Card, string, Card?>
    {
        public GetCard(string id, bool evenDeletedData = false) : base(id)
        {
            EvenDeletedData = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
