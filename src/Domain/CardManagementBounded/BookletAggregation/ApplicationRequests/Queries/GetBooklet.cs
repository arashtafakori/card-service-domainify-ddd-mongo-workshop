using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class GetBooklet :
        QueryItemRequestById<Booklet, string, Booklet?>
    {
        public bool WithIndices { get; private set; } = false;
        public GetBooklet(string id, bool withIndices = false, bool includeDeleted = false) : base(id)
        {
            WithIndices = withIndices;
            //TrackingMode = true;
            //PreventIfNoEntityWasFound = true;
            IncludeDeleted = includeDeleted;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
