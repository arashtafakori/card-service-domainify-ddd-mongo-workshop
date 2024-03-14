using MediatR;
using Domainify.Domain;

namespace Module.Domain.CardAggregation
{
    public class GetCardsList :
        QueryListRequest<Card, PaginatedList<CardViewModel>>
    {
        [BindTo(typeof(Card), nameof(Card.BookletId))]
        public required string BookletId { get; set; }

        [BindTo(typeof(Card), nameof(Card.IndexId))]
        public string? IndexId { get; set; }

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
