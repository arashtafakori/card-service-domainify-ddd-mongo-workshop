using MediatR;
using XSwift.Domain;

namespace Module.Domain.BookletAggregation
{
    public class GetBookletList :
        QueryListRequest<Booklet, PaginatedViewModel<BookletViewModel>>
    {
        public GetBookletList()
        {
            ValidationState.Validate();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
