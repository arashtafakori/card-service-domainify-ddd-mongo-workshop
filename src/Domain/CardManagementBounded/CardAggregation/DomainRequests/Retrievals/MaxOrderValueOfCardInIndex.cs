using Domainify.Domain;
using MediatR;

namespace Module.Domain.CardAggregation
{
    internal class MaxOrderValueOfCardInIndex :
        QueryItemRequest<Card, double>
    {
        public string BookletId { get; private set; }
        public string? IndexId { get; private set; }
        public MaxOrderValueOfCardInIndex(string bookletId, string ? indexId = null)
        {
            BookletId = bookletId;
            IndexId = indexId;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
