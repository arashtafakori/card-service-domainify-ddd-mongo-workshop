using MediatR;
using Domainify.Domain;

namespace Module.Domain.BookletAggregation
{
    public class GetIndicesList :
        QueryListRequest<Index, List<IndexViewModel>>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Id))]
        public required string BookletId { get; set; }

        public bool? IsDeleted { get; set; }
        public string? SearchValue { get; set; } = string.Empty;

        public GetIndicesList(string bookletId)
        {
            BookletId = bookletId;
            ValidationState.Validate();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
