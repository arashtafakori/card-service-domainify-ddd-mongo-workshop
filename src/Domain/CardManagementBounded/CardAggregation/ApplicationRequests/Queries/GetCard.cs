using Domainify.Domain;
using MediatR;

namespace Domain.CardAggregation
{
    public class GetCard :
        QueryItemRequestById<Card, string, Card?>
    {
        public GetCard(string id, bool includeDeleted = false) : base(id)
        {
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
