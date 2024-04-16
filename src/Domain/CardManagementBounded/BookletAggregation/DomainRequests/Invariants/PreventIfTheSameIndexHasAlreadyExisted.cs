using Domainify;
using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    internal class PreventIfTheSameIndexHasAlreadyExisted
        : InvariantRequest<Index>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Id))]
        public string? BookletId { get; private set; }
        public Index Index { get; private set; }
        public PreventIfTheSameIndexHasAlreadyExisted(Index index, string? bookletId = null)
        {
            Index = index;
            BookletId = bookletId;
        }
        public override IIssue? GetIssue()
        {
            return new AnEntityWithTheseUniquenessConditionsHasAlreadyExisted(
                    typeof(Index).Name, Description);
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
