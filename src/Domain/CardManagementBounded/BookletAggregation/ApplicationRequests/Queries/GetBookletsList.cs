using MediatR;
using Domainify.Domain;

namespace Domain.BookletAggregation
{
    public class GetBookletsList :
        QueryListRequest<Booklet, PaginatedList<BookletViewModel>>
    {
        public bool? IsDeleted { get; set; }
        public string? SearchValue { get; set; } = string.Empty;

        public GetBookletsList()
        {
            ValidationState.Validate();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
