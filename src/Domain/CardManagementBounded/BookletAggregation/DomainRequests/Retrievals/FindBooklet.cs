using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    internal class FindBooklet :
        QueryItemRequestById<Booklet, string, Booklet?>
    {
        public bool WithIndices { get; private set; } = false;
        public FindBooklet(string id, bool withIndices = false, bool includeDeleted = false) : base(id)
        {
            WithIndices = withIndices;
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
