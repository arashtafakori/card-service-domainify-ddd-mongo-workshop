using Domainify;
using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    internal class PreventIfTheSameIndexHasAlreadyExisted
        : InvariantRequest<Index>
    {
        public Index Index { get; private set; }
        public PreventIfTheSameIndexHasAlreadyExisted(Index index)
        {
            Index = index;
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
