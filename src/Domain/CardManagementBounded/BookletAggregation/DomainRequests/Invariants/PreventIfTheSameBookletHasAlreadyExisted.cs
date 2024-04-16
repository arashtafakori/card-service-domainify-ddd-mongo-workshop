using Domainify;
using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    internal class PreventIfTheSameBookletHasAlreadyExisted
        : InvariantRequest<Booklet>
    {
        public Booklet Booklet { get; private set; }
        public PreventIfTheSameBookletHasAlreadyExisted(Booklet booklet)
        {
            Booklet = booklet;
        }
        public override IIssue? GetIssue()
        {
            return new AnEntityWithTheseUniquenessConditionsHasAlreadyExisted(
                    typeof(Booklet).Name, Description);
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
