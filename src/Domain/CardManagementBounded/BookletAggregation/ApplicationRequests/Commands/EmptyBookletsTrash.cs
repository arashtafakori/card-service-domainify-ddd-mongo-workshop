using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class EmptyBookletsTrash :
        CommandRequest<Booklet>
    {
        public EmptyBookletsTrash()
        { 
            ValidationState.Validate();
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
