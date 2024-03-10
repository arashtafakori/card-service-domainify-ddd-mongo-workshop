using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    internal class MaxOrderValueOfCardInIndex :
        QueryItemRequest<Card, long>
    {
        public string? IndexId { get; private set; }
        public MaxOrderValueOfCardInIndex(string? indexId = null)
        {
            IndexId = indexId;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
