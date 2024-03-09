using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class RestoreIndex :
        RequestToRestoreById<Index, string>
    {
        public RestoreIndex(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Index> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var index = (await mediator.Send(
                new FindIndex(Id, evenDeletedData: true)))!;

            InvariantState.AddAnInvariantRequest(new PreventIfTheSameIndexHasAlreadyExisted(index));
            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}
