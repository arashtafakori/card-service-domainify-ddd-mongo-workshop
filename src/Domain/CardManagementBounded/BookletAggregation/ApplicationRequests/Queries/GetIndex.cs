using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class GetIndex :
        QueryItemRequestById<Index, string, Index?>
    {
        public GetIndex(string id, bool evenDeletedData = false) : base(id)
        {
            //PreventIfNoEntityWasFound = true;
            IncludeDeleted = evenDeletedData;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
