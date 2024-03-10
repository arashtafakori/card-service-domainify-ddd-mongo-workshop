using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class EmptyIndicesTrash :
        CommandRequest<Index>
    {
        [BindTo(typeof(Booklet), nameof(Booklet.Id))]
        public string BookletId { get; private set; }
        public EmptyIndicesTrash(string bookletId)
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
