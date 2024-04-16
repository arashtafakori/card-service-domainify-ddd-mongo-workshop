using Domainify.Domain;
using MediatR;

namespace Domain.CardAggregation
{
    internal class FindCard :
        QueryItemRequestById<Card, string, Card?>
    {
        public FindCard(string id, bool includeDeleted = false) : base(id)
        {
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
