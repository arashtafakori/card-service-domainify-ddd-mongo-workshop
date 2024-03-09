using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class MaxOrderValueOfBookletIndices :
        QueryItemRequest<Booklet, int>
    {
        public string BookletId { get; private set; }
        public MaxOrderValueOfBookletIndices(string bookletId)
        {
            BookletId = bookletId;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
