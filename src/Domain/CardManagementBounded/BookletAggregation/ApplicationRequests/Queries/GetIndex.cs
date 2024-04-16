using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class GetIndex :
        QueryItemRequestById<Index, string, Index?>
    {
        public GetIndex(string id, bool includeDeleted = false) : base(id)
        {
            //PreventIfNoEntityWasFound = true;
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
