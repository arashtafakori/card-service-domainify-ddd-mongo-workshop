using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class FindIndex :
        QueryItemRequestById<Index, string, Index?>
    {
        public FindIndex(string id, bool evenDeletedData = false) : base(id)
        {
            EvenDeletedData = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
