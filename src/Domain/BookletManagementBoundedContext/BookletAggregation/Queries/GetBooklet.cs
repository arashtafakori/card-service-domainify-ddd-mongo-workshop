using XSwift.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class GetBooklet :
        QueryItemRequestById<Booklet, string, BookletViewModel?>
    {
        public GetBooklet(string id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
