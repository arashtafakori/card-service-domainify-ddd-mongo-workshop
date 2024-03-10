using MediatR;
using Domainify.Domain;

namespace Module.Domain.CardAggregation
{
    public class GetCardsList :
        QueryListRequest<Card, PaginatedViewModel<CardViewModel>>
    {
        public bool? IsDeleted { get; set; }
        public string? SearchValue { get; set; } = string.Empty;

        public GetCardsList()
        {
            ValidationState.Validate();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
