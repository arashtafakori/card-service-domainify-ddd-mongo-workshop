using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class FindBooklet :
        QueryItemRequestById<Booklet, string, Booklet?>
    {
        public bool WithIndices { get; private set; } = false;
        public FindBooklet(string id, bool withIndices = false, bool evenDeletedData = false) : base(id)
        {
            WithIndices = withIndices;
            EvenDeletedData = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
