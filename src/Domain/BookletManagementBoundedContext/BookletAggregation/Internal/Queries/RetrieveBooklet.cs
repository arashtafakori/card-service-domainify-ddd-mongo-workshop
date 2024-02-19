using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class RetrieveBooklet :
        QueryItemRequestById<Booklet, string, Booklet?>
    {
        public RetrieveBooklet(string id,
            bool evenArchivedData = false)
            : base(id)
        {
            TrackingMode = true;
            PreventIfNoEntityWasFound = true;
            EvenArchivedData = evenArchivedData;
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
