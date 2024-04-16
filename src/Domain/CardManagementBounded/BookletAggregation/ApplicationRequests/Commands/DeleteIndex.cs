using Domainify.Domain;
using MediatR;

namespace Domain.BookletAggregation
{
    public class DeleteIndex :
        RequestToDeleteById<Index, string>
    {
        public DeleteIndex(string id) 
            : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Index> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);

            var index = (await mediator.Send(
                new FindIndex(Id, includeDeleted: true)))!;
            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}