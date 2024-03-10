using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class MaxOrderValueOfIndexInBooklet :
        QueryItemRequest<Booklet, int>
    {
        public string BookletId { get; private set; }
        public MaxOrderValueOfIndexInBooklet(string bookletId)
        {
            BookletId = bookletId;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
