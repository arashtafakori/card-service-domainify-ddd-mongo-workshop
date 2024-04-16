using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    internal class FindIndex :
        QueryItemRequestById<Index, string, Index?>
    {
        public FindIndex(string id, bool includeDeleted = false) : base(id)
        {
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
