using Domainify.Domain;
using MediatR;

namespace Module.Domain.BookletAggregation
{
    public class EditIndexName :
        RequestToUpdateById<Index, string>
    {
        [BindTo(typeof(Index), nameof(Index.Name))]
        public string Name { get; private set; }
 
        public EditIndexName(string id, string name) 
            : base(id)
        {
            Name = name.Trim();
            ValidationState.Validate();
        }

        public override async Task<Index> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var index = (await mediator.Send(new FindIndex(id: Id)))!;
            index.SetName(Name);

            InvariantState.AddAnInvariantRequest(
                new PreventIfTheSameIndexHasAlreadyExisted(index));
            await InvariantState.AssestAsync(mediator);
 
            await base.ResolveAsync(mediator, index);
            return index;
        }
    }
}
